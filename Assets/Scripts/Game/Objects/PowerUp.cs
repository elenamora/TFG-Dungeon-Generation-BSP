using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    public PowerUpData data;

    private Player player;

    [SerializeField]
    private Inventory inventory;

    [SerializeField]
    private InventoryItem inventoryItem;

   

    void Start()
    {
        player = FindObjectOfType<Player>().GetComponent<Player>();
    }

    void Update()
    {
        
    }

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
            //player = collision.gameObject.GetComponent<Player>();

            //player.currentHealth = player.IncreaseHealth(data.extraHealth);

            //player.currentEnergy = player.IncreaseEnergy(data.extraEnergy);

            AddItemToInventory();

            Destroy(gameObject);
        }
    }
}
