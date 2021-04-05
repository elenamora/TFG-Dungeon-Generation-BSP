
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : Interactable
{
    public Item item;
    public Inventory inventory;

    public SpriteRenderer itemInChest;

    public bool isOpen;

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
                animator.SetBool("open", true);
                StartCoroutine(OpenCo());
                StartCoroutine(OpenedCo());
            }
        }
    }

    public void OpenChest()
    {
        inventory.AddItem(item);
        inventory.item = item;
        isOpen = true;
        itemInChest.sprite = inventory.item.itemSprite;
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

}
