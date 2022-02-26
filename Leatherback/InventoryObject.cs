
using System;

using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class InventoryObject {

    public bool isTexture;
    public int amount;

    public virtual Texture2D GetTexture () {

        var texture = new Texture2D(4, 4);
        texture.SetPixel(1, 1, Color.black);
        texture.Apply();
        return texture;
    }

    public virtual Mesh GetMesh () {

        return new Mesh();
    }
}
