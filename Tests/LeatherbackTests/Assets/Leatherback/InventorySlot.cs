
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

    public InventoryManager manager;

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
        objectDisplayObject.transform.localScale = new Vector3(16, 16, 16);
        objectDisplayObject.transform.Translate(Vector3.back * 1);
        objectDisplayObject.transform.Rotate(new Vector3(-16, 45, -16));
        objectDisplayObject.layer = LayerMask.NameToLayer("UI");
        objectDisplayObject.AddComponent<MeshFilter>().mesh = objectDisplay;
        objectDisplayObject.AddComponent<MeshRenderer>().material =
            InventoryPrefix.instance.objectDisplayMaterial;
        ObjectDisplay = objectDisplayObject;

        GameObject amountDisplayObject = new GameObject("Amount Display");
        amountDisplayObject.transform.SetParent(transform);
        amountDisplayObject.transform.localScale = new Vector3(1, 1, 1);
        amountDisplayObject.transform.Translate(Vector3.back * 2);
        amountDisplay = amountDisplayObject.AddComponent<Text>();
        amountDisplay.font = InventoryPrefix.instance.font;
        amountDisplay.color = InventoryPrefix.instance.fontColor;
        amountDisplay.fontSize = manager.inventory.fontSize;
        amountDisplay.alignment = manager.inventory.fontAlignment;
        amountDisplayObject.GetComponent<RectTransform>().sizeDelta = new Vector2(
            manager.inventory.slotSize.x, manager.inventory.slotSize.y);

        GameObject percentDisplayObject = new GameObject("Percent Display");
        percentDisplayObject.transform.SetParent(transform);
        percentDisplayObject.transform.localScale = new Vector3(.9f, .9f, .9f);
        percentDisplay = percentDisplayObject.AddComponent<Image>();
        percentTransform = percentDisplay.GetComponent<RectTransform>();
        // Dies ist vorerst 0, wird bald behoben. //
        percentTransform.anchoredPosition = new Vector2(0, -manager.inventory.slotSize.y/2-(-1));
        percentTransform.sizeDelta = new Vector2(manager.inventory.slotSize.x, 2);
    }

    public virtual Action<InventorySlot, bool> onSlotClick => null;

    public void OnPointerClick (PointerEventData e) {

        // Uncomment these lines to enable sounds, Reverb is required.
        // if (InventoryPrefix.instance.clickSound != "")
        //     ReverbAudioManager.Play(InventoryPrefix.instance.clickSound);

        if (onSlotClick != null) onSlotClick(this, e.button == PointerEventData.InputButton.Left);
        else manager.OnSlotClicked(this, e.button == PointerEventData.InputButton.Left);
    }
    public void OnPointerEnter (PointerEventData e) {

        displayImage.sprite = highlightedImage;
        displayImage.color = highlightedColor;

        // Uncomment these lines to enable sounds, Reverb is required.
        // if (InventoryPrefix.instance.highlightSound != "")
        //     ReverbAudioManager.Play(InventoryPrefix.instance.highlightSound);
    }
    public void OnPointerExit (PointerEventData e) {

        displayImage.sprite = idleImage;
        displayImage.color = idleColor;
    }

    public GameObject ObjectDisplay;

    private RawImage textureDisplay;
    private Mesh objectDisplay;
    private Text amountDisplay;
    private Image percentDisplay;
    private RectTransform percentTransform;

    public InventoryObject data;

    public bool isEmpty => data == null;
    public bool isFull => data != null;
    public bool CanMerge (InventoryObject iobj) => data.CanMerge(iobj);
    public InventoryObject Merge (InventoryObject iobj) => data.Merge(iobj);
    public InventoryObject TakeFrom (InventoryObject iobj, int i) => data.TakeFrom(iobj, i);

    public void Render () {

        ClearDisplay();

        if (data == null) return;

        if (data.isTexture) {

            textureDisplay.texture = data.GetTexture();
            textureDisplay.color = Color.white;

        } else data.AsignMesh(objectDisplay);

        amountDisplay.text = data.amount == 0 ? "" : data.amount.ToString();

        percentDisplay.color = Color.Lerp(
            InventoryPrefix.instance.zeroPercent, InventoryPrefix.instance.hundredPercent,
            data.percent);

        float left = (float)manager.inventory.slotSize.x * data.percent;
        float right = Mathf.Abs(manager.inventory.slotSize.x - left / 2);
        // Dies ist vorerst 0, wird bald behoben. //
        percentTransform.anchoredPosition = new Vector2(0, -manager.inventory.slotSize.y/2-(-1));
        percentTransform.sizeDelta = new Vector2(left, 2);
    }

    public void ClearDisplay () {

        textureDisplay.color = Color.clear;
        objectDisplay.Clear();
        amountDisplay.text = "";
        percentDisplay.color = Color.clear;
    }
}
