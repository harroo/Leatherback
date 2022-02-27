
using System;

using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class InventoryObject {

    public bool isTexture;
    public ulong typeId;
    public bool stackable;
    public ushort maxStackSize;

    public int amount;
    public float percent;

    public virtual Texture2D GetTexture () {

        var texture = new Texture2D(2, 2);
        texture.SetPixel(1, 1, Color.black);
        texture.SetPixel(1, 2, Color.magenta);
        texture.SetPixel(2, 1, Color.magenta);
        texture.SetPixel(2, 2, Color.black);
        texture.Apply();
        return texture;
    }

    public virtual Mesh GetMesh () {

        return new Mesh();
    }

    //should return true if the object can receive more into itself of the other
    public virtual bool CanMerge (InventoryObject other) {

        if (amount >= maxStackSize) return false;
        if (!other.stackable || !stackable) return false;

        return other.typeId == typeId;
    }

    //should merge the other into this and return whats left, null is ok if none left
    public virtual InventoryObject Merge (InventoryObject other) {

        int amountToTake = maxStackSize - amount;
        if (amountToTake > other.amount) amountToTake = other.amount;

        amount += amountToTake;
        other.amount -= amountToTake;

        if (other.amount <= 0) return null;
        else return other;
    }
}
