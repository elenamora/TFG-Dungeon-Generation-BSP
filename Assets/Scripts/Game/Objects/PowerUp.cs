using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private Inventory inventory;

    [SerializeField]
    private InventoryItem inventoryItem;

    public ItemManager itemManager;

    public ParticleSystem ps;

    /*
     * Function called when the player picks up a power up object.
     * It will add the picked up item to the player's inventory
     */
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
    
    /*
     * Manages the collision of the player with any powerUp. 
     * If there's a collision the function AddItemToInventory will be called, some particles will be instantiated and,
     * finally, the picked up object will be destroy.
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AddItemToInventory();
            Instantiate(ps, transform.position, transform.rotation);
            itemManager.pickedItems += 1;
            Destroy(gameObject);
        }
    }
}
