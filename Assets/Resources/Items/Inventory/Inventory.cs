using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "My Game/ Inventory / Inventory")]
public class Inventory : ScriptableObject
{
    public InventoryItem item;

    public List<InventoryItem> inventoryItems;


    public void AddInventoryItem(InventoryItem item)
    {
        // If we don't have the item in our inventory we will add it
        if (!inventoryItems.Contains(item))
        {
            inventoryItems.Add(item);
            item.quantity += 1;
        }
        else
        {
            item.quantity++;
        }
    }

}
