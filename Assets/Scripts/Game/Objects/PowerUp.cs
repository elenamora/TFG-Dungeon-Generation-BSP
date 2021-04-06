﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private Inventory inventory;

    [SerializeField]
    private InventoryItem inventoryItem;

    public void AddItemToInventory()
    {
        if (inventory && inventoryItem)
        {
            if (inventory.inventoryItems.Contains(inventoryItem)) { inventoryItem.quantity++; }

            else
            {
                inventory.AddInventoryItem(inventoryItem);
                inventoryItem.quantity += 1;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AddItemToInventory();
            Destroy(gameObject);
        }
    }
}
