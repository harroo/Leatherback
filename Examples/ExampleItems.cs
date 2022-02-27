
using UnityEngine;

[System.Serializable]
public class Sword : InventoryObject {

    public Sword () {

        isTexture = true;
        typeId = 0;
        stackable = true;
        maxStackSize = InventoryPrefix.instance.defaultStackSize;
    }

    public override Texture2D GetTexture () {

        return Object.FindObjectOfType<ExampleInventory>().swordTexture;
    }
}
