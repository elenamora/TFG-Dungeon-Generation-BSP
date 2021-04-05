using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Inventory inventory;

    [SerializeField]
    private GameObject slot;

    [SerializeField]
    private GameObject panel;

    [SerializeField]
    private Text description;

    public void SetText(string description)
    {
        //this.description.text = description;
    } 

    public void MakeInventorySlots()
    {
        if (inventory)
        {
            for(int i = 0; i < inventory.inventoryItems.Count; i++)
            {
                GameObject temp =
                    Instantiate(slot, panel.transform.position, Quaternion.identity);

                temp.transform.SetParent(panel.transform);

                InventorySlot newSlot = temp.GetComponent<InventorySlot>();

                if (newSlot) { newSlot.SetUp(inventory.inventoryItems[i], this);}  
            }
        }
    }

    public void ClearInventorySlots()
    {
        // We'll create the slots from scratch every time we want to update them so we don't keep old slotsc
        for (int i = 0; i < panel.transform.childCount; i++)
        {
            Destroy(panel.transform.GetChild(i).gameObject);
        }
    }

    // OnEnable is called every time an object goes from being disabled to enabled
    void Start()
    {
        ClearInventorySlots();
        //SetText("");
        MakeInventorySlots();
    }

    void Update()
    {
        ClearInventorySlots();
        //SetText("");
        MakeInventorySlots();
    }

}
