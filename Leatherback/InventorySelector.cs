
using UnityEngine;
using UnityEngine.UI;

public class InventorySelector : MonoBehaviour {

    public static InventorySelector instance;

    public static void Render () { instance._Render(); }
    public static InventoryObject selectedData {
        get { return instance.data; }
        set { instance.data = value; }
    }

    private RectTransform _rectTransform;

    private InventoryManager manager;

    private Canvas canvas;

    public void Initialize (InventoryManager manager) {

        if (instance != null) {

            Destroy(gameObject); return;
        }

        instance = this;
        this.manager = manager;

        _rectTransform = GetComponent<RectTransform>();
        // canvas = FindObjectOfType<Canvas>();
        canvas = GameObject.Find("Inventory Canvas").GetComponent<Canvas>();
        transform.SetParent(canvas.transform);
        transform.localScale = new Vector3(1, 1, 1);

        GameObject textureDisplayObject = new GameObject("Texture Display");
        textureDisplayObject.transform.SetParent(transform);
        textureDisplayObject.transform.localScale = new Vector3(1, 1, 1);
        textureDisplay = textureDisplayObject.AddComponent<RawImage>();
        textureDisplay.raycastTarget = false;
        textureDisplayObject.GetComponent<RectTransform>().sizeDelta = new Vector2(
            manager.inventory.textureSize.x, manager.inventory.textureSize.y);

        objectDisplay = new Mesh();
        GameObject objectDisplayObject = new GameObject("Object Display");
        objectDisplayObject.transform.SetParent(transform);
        objectDisplayObject.transform.localScale = new Vector3(16, 16, 16);
        objectDisplayObject.transform.Translate(Vector3.back * 2);
        objectDisplayObject.transform.Rotate(new Vector3(-16, 45, -16));
        objectDisplayObject.layer = LayerMask.NameToLayer("UI");
        objectDisplayObject.AddComponent<MeshFilter>().mesh = objectDisplay;
        objectDisplayObject.AddComponent<MeshRenderer>().material =
            InventoryPrefix.instance.objectDisplayMaterial;

        GameObject amountDisplayObject = new GameObject("Amount Display");
        amountDisplayObject.transform.SetParent(transform);
        amountDisplayObject.transform.localScale = new Vector3(1, 1, 1);
        amountDisplayObject.transform.Translate(Vector3.back * 3);
        amountDisplay = amountDisplayObject.AddComponent<Text>();
        amountDisplay.font = InventoryPrefix.instance.font;
        amountDisplay.color = InventoryPrefix.instance.fontColor;
        amountDisplay.fontSize = manager.inventory.fontSize;
        amountDisplay.alignment = manager.inventory.fontAlignment;
        amountDisplay.raycastTarget = false;
        amountDisplayObject.GetComponent<RectTransform>().sizeDelta = new Vector2(
            manager.inventory.slotSize.x, manager.inventory.slotSize.y);

        GameObject percentDisplayObject = new GameObject("Percent Display");
        percentDisplayObject.transform.SetParent(transform);
        percentDisplayObject.transform.localScale = new Vector3(.9f, .9f, .9f);
        percentDisplay = percentDisplayObject.AddComponent<Image>();
        percentDisplay.raycastTarget = false;
        percentTransform = percentDisplay.GetComponent<RectTransform>();
        percentTransform.anchoredPosition = new Vector2(0, -manager.inventory.slotSize.y/2-1);
        percentTransform.sizeDelta = new Vector2(manager.inventory.slotSize.x, 2);
    }

    private RawImage textureDisplay;
    private Mesh objectDisplay;
    private Text amountDisplay;
    private Image percentDisplay;
    private RectTransform percentTransform;

    public InventoryObject data;

    public bool isEmpty => data == null;
    public bool isFull => data != null;

    public void _Render () {

        ClearDisplay();

        if (data == null) return;

        if (data.isTexture) {

            textureDisplay.texture = data.GetTexture();
            textureDisplay.color = Color.white;

        } else data.AsignMesh(objectDisplay);

        amountDisplay.text = data.amount == 0 ? "" : data.amount.ToString();

        percentDisplay.color = Color.Lerp(
            InventoryPrefix.instance.zeroPercent, InventoryPrefix.instance.hundredPercent,
            data.percent);

        float left = (float)manager.inventory.slotSize.x * data.percent;
        float right = Mathf.Abs(manager.inventory.slotSize.x - left / 2);
        percentTransform.anchoredPosition = new Vector2(right, -manager.inventory.slotSize.y/2-1);
        percentTransform.sizeDelta = new Vector2(left, 2);
    }

    public void ClearDisplay () {

        textureDisplay.color = Color.clear;
        objectDisplay.Clear();
        amountDisplay.text = "";
        percentDisplay.color = Color.clear;
    }

    private void Update () {

        if (data == null) return;

        Vector2 pos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            Input.mousePosition,
            canvas.worldCamera,
            out pos
        );

        transform.position = canvas.transform.TransformPoint(pos);
    }
}
