
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
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            if (!isOpen)
            {
                if (HaveItemToOpen())
                {
                    animator.SetBool("open", true);
                    StartCoroutine(OpenCo());
                    StartCoroutine(OpenedCo());
                }
                else
                {
                    dialogBox.SetActive(true);
                    StartCoroutine(DialogCo());
                }
                
            }
        }
    }

    public bool HaveItemToOpen()
    {
        foreach (InventoryItem item in inventory.inventoryItems)
        {
            if (item.name == data.itemToOpen && item.quantity >= data.quantityOfItemToOpen) { return true; }
        }
        return false;
    }

    public void OpenChest()
    {
        foreach (InventoryItem item in inventory.inventoryItems)
        {
            if (item.name == data.itemToOpen)
            {
                item.quantity -= data.quantityOfItemToOpen;
            }
        }

        inventory.AddInventoryItem(item);
        inventory.item = item;
        isOpen = true;
        itemInChest.sprite = inventory.item.itemImage;

        if(inventory.item.name == "Gem")
        {
            gameState.win = true;
            gameState.LevelUp();
            StartCoroutine(EventCo());
        }
        
    }

    IEnumerator OpenCo()
    {
        yield return new WaitForSeconds(.3f);
        OpenChest();
    }

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
