
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

    public virtual void OnSlotClicked (InventorySlot slot, bool leftClicked) {

        if (slot.isEmpty && selector.isEmpty) return;

        if (leftClicked) LeftClickSlot(slot);
        else RightClickSlot(slot);
    }

    private void LeftClickSlot (InventorySlot slot) {

        if (slot.isEmpty && selector.isFull) {

            slot.data = selector.data;
            selector.data = null;
            Render(); return;
        }

        if (slot.isFull && selector.isEmpty) {

            selector.data = slot.data;
            slot.data = null;
            Render(); return;
        }

        if (slot.isFull && selector.isFull) {

            if (slot.CanMerge(selector.data)) {

                selector.data = slot.Merge(selector.data);
                Render(); return;

            } else {

                InventoryObject cache = slot.data;
                slot.data = selector.data;
                selector.data = cache;
                Render(); return;
            }
        }
    }

    private void RightClickSlot (InventorySlot slot) {

        //for now redirect to leftclick cos this aint worken
        // LeftClickSlot(slot);

        if (slot.isEmpty && selector.isFull) {

            // slot.data = selector.Clone(); slot.data.amount = 0;

            slot.data = (InventoryObject)Activator.CreateInstance(selector.data.GetType());
            // selector.data.CopyValuesTo(slot.data);
            slot.data.amount = 0;

            selector.data = slot.TakeFrom(selector.data, 1);
            Render(); return;
        }

        if (slot.isFull && selector.isEmpty) {

            // selector.data = slot.Clone(); selector.data.amount = 0;

            selector.data = (InventoryObject)Activator.CreateInstance(slot.data.GetType());
            // selector.data.CopyValuesTo(slot.data);
            selector.data.amount = 0;

            slot.data = selector.TakeFrom(slot.data, slot.data.amount / 2);
            Render(); return;
        }

        if (slot.isFull && selector.isFull) {

            if (slot.CanMerge(selector.data)) {

                selector.data = slot.TakeFrom(selector.data, 1);
                Render(); return;

            } else {

                InventoryObject cache = slot.data;
                slot.data = selector.data;
                selector.data = cache;
                Render(); return;
            }
        }
    }

    private T CastObject<T> (object input) { return (T)input; }

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
