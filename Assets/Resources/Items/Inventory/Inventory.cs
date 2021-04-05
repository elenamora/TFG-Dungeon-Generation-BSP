using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "My Game/ Inventory")]
public class Inventory : ScriptableObject
{
    public Item item;

    public List<Item> items = new List<Item>();

    public List<InventoryItem> inventoryItems = new List<InventoryItem>();

    public void AddItem(Item item)
    {
        // If we don't have the item in our inventory we will add it
        if (!items.Contains(item))
        {
            items.Add(item);
        }
    }

    public void AddInventoryItem(InventoryItem item)
    {
        // If we don't have the item in our inventory we will add it
        if (!inventoryItems.Contains(item))
        {
            inventoryItems.Add(item);
        }
    }

}
