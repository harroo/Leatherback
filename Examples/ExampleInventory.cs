
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

        //slot texture display demensiosn
        textureSize = new Vector2(30, 30),

        //slot amount text info
        fontSize = 10,
        fontAlignment = TextAnchor.LowerRight,
        //see: https://docs.unity3d.com/2019.1/Documentation/ScriptReference/TextAnchor.html

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
