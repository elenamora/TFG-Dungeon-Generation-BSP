using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    public Inventory inventory;

    public bool isOpen;
    private bool key = false;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        HaveKey();
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange && key)
        {
            if (!isOpen)
            {
                animator.SetBool("open", true);
                StartCoroutine(OpenCo());
                StartCoroutine(OpenedCo());
            }
        }
    }

    public void HaveKey()
    {
        foreach (InventoryItem item in inventory.inventoryItems)
        {
            if (item.name == "Key" && item.quantity > 0)
            {
                key = true;
            }
        }
    }

    public void OpenDoor()
    {
        foreach (InventoryItem item in inventory.inventoryItems)
        {
            if(item.name == "Key")
            {
                item.quantity -= 1;
            }
        }
        isOpen = true;
    }

    IEnumerator OpenCo()
    {
        yield return new WaitForSeconds(.3f);
        OpenDoor();
    }

    public void DoorOpened()
    {
        //inventory.item = null;
        //itemInChest.sprite = null;
    }

    IEnumerator OpenedCo()
    {
        yield return new WaitForSecondsRealtime(1f);
        DoorOpened();
    }

}
