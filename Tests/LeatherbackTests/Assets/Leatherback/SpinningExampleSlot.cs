
using System;

using UnityEngine;

public class SpinningExampleSlot : InventorySlot {

    private void Update () {

        ObjectDisplay.transform.Rotate(Vector3.up * 4 * Time.deltaTime);
    }
}
