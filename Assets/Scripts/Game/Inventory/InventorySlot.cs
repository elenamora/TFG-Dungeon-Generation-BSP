using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Text textNum;
    [SerializeField] private Image itemImage;
    //[SerializeField] private Text itemDescription;

    public InventoryItem item;
    public BasicInventoryManager basicManager;
    public InventoryManager inventoryManager;

    public void SetUp(InventoryItem newItem, BasicInventoryManager newManager )
    {
        item = newItem;
        basicManager = newManager;

        if (item)
        {
            itemImage.sprite = item.itemImage;
            textNum.text = "" + item.quantity;
        }
    }

    public void SetUp(InventoryItem newItem, InventoryManager newManager)
    {
        item = newItem;
        inventoryManager = newManager;

        if (item)
        {
            itemImage.sprite = item.itemImage;
            textNum.text = "" + item.quantity;
        }
    }

}
