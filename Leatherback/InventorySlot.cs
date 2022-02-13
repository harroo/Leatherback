
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

    private InventoryManager manager;

    private void SetManager (InventoryManager manager) {

        this.manager = manager;
    }

    private void Awake () {

        displayImage = GetComponent<Image>();
    }
    private void Start () {

        idleImage = InventoryPrefix.instance.idleImage;
        idleColor = InventoryPrefix.instance.idleColor;
        highlightedImage = InventoryPrefix.instance.highlightedImage;
        highlightedColor = InventoryPrefix.instance.highlightedColor;

        displayImage.sprite = idleImage;
        displayImage.color = idleColor;
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
}
