
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

        foreach (var slot in slots) {

            slot.ClearDisplay();
        }
    }

    public void OnSlotClicked (InventorySlot slot, bool leftClicked) {

        Debug.Log("debug test, slot clicked in slot manager");
    }
}

[Serializable]
public class Inventory {

    public Vector2 spacing, slotSize, gridSize, textureSize;

    public int fontSize;
    public TextAnchor fontAlignment;

    public char[] slots;
}
