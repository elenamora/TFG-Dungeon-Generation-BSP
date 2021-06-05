
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : Interactable
{
    [Header("Inventory")]
    public InventoryItem item;
    public Inventory inventory;

    [Header("Chest")]
    [SerializeField] private ChestData data;
    [SerializeField] private SpriteRenderer itemInChest;
    [SerializeField] private ParticleSystem ps;

    [Header("Dialog")]
    public GameObject dialogBox;
    //public Text dialogText;

    [Header("Game State")]
    public GameState gameState;
    public GameEvent winEvent;

    private bool isOpen;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the player is close enough and the player press the Space key we'll check if the chest is opened
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            if (!isOpen)
            {
                // If the chest is not open and the player has the necessary items to open it it will open
                if (HaveItemToOpen())
                {
                    animator.SetBool("open", true);
                    StartCoroutine(OpenCo());
                    StartCoroutine(OpenedCo());
                }
                // If the player doesn't have the necessary items to open it a dialog box will appear indicating
                // what items the player needs to open the chest
                else
                {
                    dialogBox.SetActive(true);
                    StartCoroutine(DialogCo());
                }
                
            }
        }
    }

    /*
     * Checks if the player has the necessary items to open the chest inside his inventory
     */
    public bool HaveItemToOpen()
    {
        foreach (InventoryItem item in inventory.inventoryItems)
        {
            if (item.name == data.itemToOpen && item.quantity >= data.quantityOfItemToOpen) { return true; }
        }
        return false;
    }

    /*
     * Function called when the player opens the chest
     */
    public void OpenChest()
    {
        // Add the item stored in the chest to the player's inventory
        foreach (InventoryItem item in inventory.inventoryItems)
        {
            if (item.name == data.itemToOpen)
            {
                item.quantity -= data.quantityOfItemToOpen;
            }
        }

        inventory.AddInventoryItem(item);
        inventory.item = item;
        Instantiate(ps, itemInChest.transform.position, itemInChest.transform.rotation);
        isOpen = true;
        itemInChest.sprite = inventory.item.itemImage;

        // If the item inside the chest is a gem it means that the player opened the big chest so the game will end and
        // the win event will be raised
        if(inventory.item.name == "Gem")
        {
            gameState.win = true;
            StartCoroutine(EventCo());
        }
        
    }

    IEnumerator OpenCo()
    {
        yield return new WaitForSeconds(.3f);
        OpenChest();
    }

    /*
     * If the player opens the chest the items that provides will disapear after a short while
     */
    public void ChestOpened()
    {
        inventory.item = null;
        itemInChest.sprite = null;
    }

    IEnumerator OpenedCo()
    {
        yield return new WaitForSecondsRealtime(1f);
        ChestOpened();
    }

    IEnumerator DialogCo()
    {
        yield return new WaitForSecondsRealtime(1f);
        dialogBox.SetActive(false);
    }

    IEnumerator EventCo()
    {
        yield return new WaitForSeconds(.4f);
        winEvent.Raise();
    }

}
