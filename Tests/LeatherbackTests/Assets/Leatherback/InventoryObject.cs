
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

    public Leatherback.MetaDataBuffer metaData;

    public void SetMetaData (byte[] newMetaData) {

        metaData = new Leatherback.MetaDataBuffer(newMetaData);
    }

    public byte[] GetMetaData () => metaData.ToArray();

    //should return what the item looks like
    public virtual Texture2D GetTexture () { return Texture2D.blackTexture; }

    //should return what the item looks like if 3d
    public virtual void AsignMesh (Mesh mesh) { }

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

    //should take an amount from 1 object to this 1
    public virtual InventoryObject TakeFrom (InventoryObject other, int amountToTake) {

        if (amountToTake > other.amount) amountToTake = other.amount;

        amount += amountToTake;
        other.amount -= amountToTake;

        if (other.amount <= 0) return null;
        else return other;
    }

    // Copies values from this instance to the "target" instance, with optional amount ignorance.
    public virtual void CopyValuesTo (InventoryObject targetObject, bool copyAmount = false) {

        targetObject.isTexture = this.isTexture;
        targetObject.typeId = this.typeId;
        targetObject.stackable = this.stackable;
        targetObject.maxStackSize = this.maxStackSize;

        if (copyAmount) targetObject.amount = this.amount;
        targetObject.percent = this.percent;
    }
}
