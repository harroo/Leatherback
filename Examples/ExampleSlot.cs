
using System;

using UnityEngine;

public class ExampleSlot : InventorySlot {

    public override Action onSlotClick => () => {

        Debug.Log("clicked from example");
    };
}
