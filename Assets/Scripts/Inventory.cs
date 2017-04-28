using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    private RectTransform inventoryRect;

    private float inventoryWidth;
    private float inventoryHeight;
    private int slots = 30;
    private int rows = 3;
    public float slotPaddingLeft;
    public float slotPaddingTop;
    public float slotSize;

    public GameObject slotPrefab;
    public GameObject panel;
    public GameObject canvas;
    public GameObject uncanvas;
    public Player player;

    private List<GameObject> allSlots;
    public bool showing = false;

    // Use this for initialization
    void Start()
    {
        CreateLayout();
        show(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp(KeyCode.E))
        {
            show(!showing);
            player.FreezeCharacter(showing);
        }
	}

    public void show(bool showing)
    {
        if (showing)
        {
            panel.transform.SetParent(canvas.transform);
        }
        else
        {
            panel.transform.SetParent(uncanvas.transform);
        }
        this.showing = showing;
    }

    private void CreateLayout()
    {
        allSlots = new List<GameObject>();

        inventoryWidth = (slots / rows) * (slotSize + slotPaddingLeft) + slotPaddingLeft;
        inventoryHeight = rows * (slotSize + slotPaddingTop) + slotPaddingTop;

        inventoryRect = GetComponent<RectTransform>();
        inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, inventoryWidth);
        inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, inventoryHeight);

        int columns = slots / rows;

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                GameObject newSlot = (GameObject)Instantiate(slotPrefab);
                RectTransform slotRect = newSlot.GetComponent<RectTransform>();
                newSlot.name = "Slot";
                newSlot.transform.SetParent(this.transform.parent);

                slotRect.localPosition = inventoryRect.localPosition + new Vector3(slotPaddingLeft + (x + 1) + (slotSize * x), -slotPaddingTop * (y + 1) - (slotSize * y));

                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize);
                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize);

                allSlots.Add(newSlot);
            }
        }
    }
}
