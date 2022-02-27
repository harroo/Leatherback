
using UnityEngine;
using UnityEngine.UI;

using System;
using System.Reflection;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour {

    public virtual Inventory inventory => null;

    public virtual void SetSlotTypes () {}


    private List<InventorySlot> slots
        = new List<InventorySlot>();


    private Dictionary<char, Type> slotTypes
        = new Dictionary<char, Type>();

    public void AddSlot (char key, Type value) {

        slotTypes.Add(key, value);
    }

    private RectTransform rectTransform;
    private GridLayoutGroup layoutGroup;

    private void Awake () {

        rectTransform = GetComponent<RectTransform>();
        layoutGroup = gameObject.AddComponent<GridLayoutGroup>();
    }

    private void Start () {

        AddSlot('S', typeof(InventorySlot));
        SetSlotTypes();

        Construct(inventory);
    }

    private bool constructed = false;

    private void Construct (Inventory inventory) {

        if (constructed) return; constructed = true;

        rectTransform.sizeDelta = new Vector2(
            (inventory.slotSize.x + inventory.spacing.x) * inventory.gridSize.x,
            (inventory.slotSize.y + inventory.spacing.y) * inventory.gridSize.y
        );

        layoutGroup.cellSize = inventory.slotSize;
        layoutGroup.spacing = inventory.spacing;

        foreach (var slotKey in inventory.slots) {

            GameObject slot = new GameObject("Slot-Type: '" + slotKey + "'");
            slot.transform.SetParent(transform);
            slot.transform.localScale = new Vector3(1, 1, 1);

            if (slotKey == ' ') {

                slot.AddComponent<Image>().color = Color.clear;
                continue;
            }

            var behaviour = slot.AddComponent(slotTypes[slotKey]);
            behaviour.SendMessage("Initialize", this);
            slots.Add((InventorySlot)behaviour);
        }

        GameObject selectorObject = new GameObject("Selector Bob");
        selectorObject.AddComponent<InventorySelector>().Initialize(this);

        Render();
    }

    private InventorySelector selector => InventorySelector.instance;

    public void OnSlotClicked (InventorySlot slot, bool leftClicked) {

        //if slot is empty and selector is empty
        if (slot.isEmpty && selector.isEmpty) return;
            //return

        //if slot is empty and selector is full
        if (slot.isEmpty && selector.isFull) {
            //mv selector into slot
            slot.data = selector.data;
            selector.data = null;
            //return
            Render(); return;
        }

        //if slot is full and selector is empty
        if (slot.isFull && selector.isEmpty) {
            //mv slot to selector
            selector.data = slot.data;
            slot.data = null;
            //return
            Render(); return;
        }

        //if slot is full and selector is full
        if (slot.isFull && selector.isFull) {
            //if they can merge
            if (slot.CanMerge(selector.data)) {
                //merge
                selector.data = slot.Merge(selector.data);
                //return
                Render(); return;

            } else { //if they cant merge
                //swap
                InventoryObject cache = slot.data;
                slot.data = selector.data;
                selector.data = cache;
                //return
                Render(); return;
            }
        }
    }

    public void Render () {

        foreach (var slot in slots)
            slot.Render();

        InventorySelector.Render();
    }

    public void Add (InventoryObject iobj) {

        foreach (var slot in slots) {

            if (iobj == null) break;

            if (slot.isEmpty) {

                slot.data = iobj; iobj = null;

            } else if (slot.CanMerge(iobj)) iobj = slot.Merge(iobj);
        }

        if (iobj != null)
            Debug.Log("Object did not completely fit!");

        Render();
    }
}

[Serializable]
public class Inventory {

    public Vector2 spacing, slotSize, gridSize, textureSize;

    public int fontSize;
    public TextAnchor fontAlignment;

    public char[] slots;
}
