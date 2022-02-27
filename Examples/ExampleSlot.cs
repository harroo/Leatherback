
using System;

using UnityEngine;

public class ExampleSlot : InventorySlot {

    public override Action<InventorySlot> onSlotClick => (InventorySlot slot) => {

        slot.manager.Add(new Sword(){amount=UnityEngine.Random.Range(1, 4)});
    };
}
