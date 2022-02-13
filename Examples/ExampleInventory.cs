
using System;

using UnityEngine;

public class ExampleInventory : InventoryManager {

    //define inventory here
    public override Inventory inventory => new Inventory {

        //spacing between slots
        spacing = new Vector2(4, 4),
        //size of slots
        slotSize = new Vector2(32, 32),
        //demensions of inventory grid
        gridSize = new Vector2(5, 4),

        //slot map
        slots = new char[] {

            'S','S','S','S','S',
            'S','S','S','S','S',
            'S','S','S','S','S',
            ' ',' ',' ',' ',' ',
            'H','H','H','H','H',
        },
    };

    //called at start to define symbols for slot behvaiours
    public override void SetSlotTypes () {

        AddSlot('H', typeof(ExampleSlot));
    }
}
