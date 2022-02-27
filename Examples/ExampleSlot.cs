
using System;

using UnityEngine;

public class ExampleSlot : InventorySlot {

    public override Action<InventorySlot> onSlotClick => (InventorySlot slot) => {

        if (UnityEngine.Random.Range(0, 2) == 0)
            slot.manager.Add(new Sword(){amount=UnityEngine.Random.Range(1, 4)});
        else
            slot.manager.Add(new Box(){amount=UnityEngine.Random.Range(1, 4)});
    };
}
