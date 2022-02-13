
using UnityEngine;
using UnityEngine.UI;

public class InventoryPrefix : MonoBehaviour {

    public static InventoryPrefix instance;
    private void Awake () { instance = this; }

    public Sprite backgroundImage;
    public Color backgroundColor = new Color(1, 1, 1, 1);

    [Space()]
    public Sprite idleImage;
    public Color idleColor = new Color(1, 1, 1, 1);

    [Space()]
    public Sprite highlightedImage;
    public Color highlightedColor = new Color(1, 1, 1, 1);

    // Sounds will require Reverb.
    // Here --> https://github.com/harroo/Reverb
    // To use this, uncomment the Reverb Function-calls in
    // "InventorySlot.cs"
    [Space()]
    public string highlightSound = "";
    public string clickSound = "";

    [Space()]
    public Font font;
    public Color fontColor = new Color(0, 0, 0, 1);

    [Space()]
    public ushort defaultStackSize = 64;
}
