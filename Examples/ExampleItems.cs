
using System;

using UnityEngine;

[Serializable]
public class Sword : InventoryObject {

    public Sword () {

        isTexture = true;
        typeId = 0;
        stackable = true;
        maxStackSize = InventoryPrefix.instance.defaultStackSize;

        percent = 0.5f;
    }

    public override Texture2D GetTexture () {

        return GameObject.FindObjectOfType<ExampleInventory>().swordTexture;
    }
}

[Serializable]
public class Box : InventoryObject {

    public Box () {

        isTexture = false;
        typeId = 1;
        stackable = true;
        maxStackSize = InventoryPrefix.instance.defaultStackSize;
    }

    public override void AsignMesh (Mesh mesh) {

        mesh.vertices = new Vector3[] {

            // PX Face.
            new Vector3(0.5f, -0.5f, -0.5f),
            new Vector3(0.5f,  0.5f, -0.5f),
            new Vector3(0.5f,  0.5f,  0.5f),
            new Vector3(0.5f, -0.5f,  0.5f),

            // NX Face.
            new Vector3(-0.5f, -0.5f,  0.5f),
            new Vector3(-0.5f,  0.5f,  0.5f),
            new Vector3(-0.5f,  0.5f, -0.5f),
            new Vector3(-0.5f, -0.5f, -0.5f),

            // PY Face.
            new Vector3(-0.5f, 0.5f,  0.5f),
            new Vector3( 0.5f, 0.5f,  0.5f),
            new Vector3( 0.5f, 0.5f, -0.5f),
            new Vector3(-0.5f, 0.5f, -0.5f),

            // NY Face.
            new Vector3(-0.5f, -0.5f, -0.5f),
            new Vector3( 0.5f, -0.5f, -0.5f),
            new Vector3( 0.5f, -0.5f,  0.5f),
            new Vector3(-0.5f, -0.5f,  0.5f),

            // PZ Face.
            new Vector3( 0.5f, -0.5f, 0.5f),
            new Vector3( 0.5f,  0.5f, 0.5f),
            new Vector3(-0.5f,  0.5f, 0.5f),
            new Vector3(-0.5f, -0.5f, 0.5f),

            // NZ Face.
            new Vector3(-0.5f, -0.5f, -0.5f),
            new Vector3(-0.5f,  0.5f, -0.5f),
            new Vector3( 0.5f,  0.5f, -0.5f),
            new Vector3( 0.5f, -0.5f, -0.5f),
        };

        mesh.triangles = new int[] {

            // PX Face.
            0, 1, 2,  0, 2, 3,

            // NX Face.
            4, 5, 6,  4, 6, 7,

            // PY Face.
            8, 9, 10,  8, 10, 11,

            // NY Face.
            12, 13, 14,  12, 14, 15,

            // PZ Face.
            16, 17, 18,  16, 18, 19,

            // NZ Face.
            20, 21, 22,  20, 22, 23,
        };

        mesh.uv = new Vector2[] {

            // PX Face.
            new Vector2(1 * 0 + 1, 1 * 0),
            new Vector2(1 * 0 + 1, 1 * 0 + 1),
            new Vector2(1 * 0, 1 * 0 + 1),
            new Vector2(1 * 0, 1 * 0),

            // NX Face.
            new Vector2(1 * 0 + 1, 1 * 0),
            new Vector2(1 * 0 + 1, 1 * 0 + 1),
            new Vector2(1 * 0, 1 * 0 + 1),
            new Vector2(1 * 0, 1 * 0),

            // PY Face.
            new Vector2(1 * 0 + 1, 1 * 0),
            new Vector2(1 * 0 + 1, 1 * 0 + 1),
            new Vector2(1 * 0, 1 * 0 + 1),
            new Vector2(1 * 0, 1 * 0),

            // NY Face.
            new Vector2(1 * 0 + 1, 1 * 0),
            new Vector2(1 * 0 + 1, 1 * 0 + 1),
            new Vector2(1 * 0, 1 * 0 + 1),
            new Vector2(1 * 0, 1 * 0),

            // PZ Face.
            new Vector2(1 * 0 + 1, 1 * 0),
            new Vector2(1 * 0 + 1, 1 * 0 + 1),
            new Vector2(1 * 0, 1 * 0 + 1),
            new Vector2(1 * 0, 1 * 0),

            // NZ Face.
            new Vector2(1 * 0 + 1, 1 * 0),
            new Vector2(1 * 0 + 1, 1 * 0 + 1),
            new Vector2(1 * 0, 1 * 0 + 1),
            new Vector2(1 * 0, 1 * 0),
        };

        mesh.RecalculateNormals();
    }
}
