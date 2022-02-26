
using System;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class InventorySlot :
        MonoBehaviour, IPointerClickHandler,
        IPointerEnterHandler, IPointerExitHandler {

    public Sprite idleImage, highlightedImage;
    public Color idleColor, highlightedColor;

    private Image displayImage;
    private RectTransform _rectTransform;

    private InventoryManager manager;

    private void Initialize (InventoryManager manager) {

        this.manager = manager;

        displayImage = GetComponent<Image>();
        _rectTransform = GetComponent<RectTransform>();

        idleImage = InventoryPrefix.instance.idleImage;
        idleColor = InventoryPrefix.instance.idleColor;
        highlightedImage = InventoryPrefix.instance.highlightedImage;
        highlightedColor = InventoryPrefix.instance.highlightedColor;

        displayImage.sprite = idleImage;
        displayImage.color = idleColor;

        GameObject textureDisplayObject = new GameObject("Texture Display");
        textureDisplayObject.transform.SetParent(transform);
        textureDisplayObject.transform.localScale = new Vector3(1, 1, 1);
        textureDisplay = textureDisplayObject.AddComponent<RawImage>();
        textureDisplayObject.GetComponent<RectTransform>().sizeDelta = new Vector2(
            manager.inventory.textureSize.x, manager.inventory.textureSize.y);

        objectDisplay = new Mesh();
        GameObject objectDisplayObject = new GameObject("Object Display");
        objectDisplayObject.transform.SetParent(transform);
        objectDisplayObject.transform.localScale = new Vector3(1, 1, 1);
        objectDisplayObject.AddComponent<MeshFilter>().mesh = objectDisplay;
        objectDisplayObject.AddComponent<MeshRenderer>().material =
            InventoryPrefix.instance.objectDisplayMaterial;

        GameObject textObject = new GameObject("Text Object");
        textObject.transform.SetParent(transform);
        textObject.transform.localScale = new Vector3(1, 1, 1);
        amountDisplay = textObject.AddComponent<Text>();
        amountDisplay.font = InventoryPrefix.instance.font;
        amountDisplay.color = InventoryPrefix.instance.fontColor;
        amountDisplay.fontSize = manager.inventory.fontSize;
        amountDisplay.alignment = manager.inventory.fontAlignment;
        textObject.GetComponent<RectTransform>().sizeDelta = new Vector2(
            manager.inventory.slotSize.x, manager.inventory.slotSize.y);
    }

    public virtual Action onSlotClick => null;

    public void OnPointerClick (PointerEventData e) {

        // Uncomment these lines to enable sounds, Reverb is required.
        // if (OcularityPrefix.instance.clickSound != "")
        //     ReverbAudioManager.Play(OcularityPrefix.instance.clickSound);

        if (onSlotClick != null) onSlotClick();
        else manager.OnSlotClicked(this, e.button == PointerEventData.InputButton.Left);
    }
    public void OnPointerEnter (PointerEventData e) {

        displayImage.sprite = highlightedImage;
        displayImage.color = highlightedColor;

        // Uncomment these lines to enable sounds, Reverb is required.
        // if (OcularityPrefix.instance.highlightSound != "")
        //     ReverbAudioManager.Play(OcularityPrefix.instance.highlightSound);
    }
    public void OnPointerExit (PointerEventData e) {

        displayImage.sprite = idleImage;
        displayImage.color = idleColor;
    }

    private RawImage textureDisplay;
    private Mesh objectDisplay;
    private Text amountDisplay;

    public InventoryObject data;

    public void Render () {

        ClearDisplay();

        if (data.isTexture) textureDisplay.texture = data.GetTexture();
        else objectDisplay = data.GetMesh();

        amountDisplay.text = data.amount == 0 ? "" : data.amount.ToString();
    }

    public void ClearDisplay () {

        textureDisplay.color = Color.clear;
        objectDisplay.Clear();
        amountDisplay.text = "";
    }
}
