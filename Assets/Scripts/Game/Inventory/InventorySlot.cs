using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField]
    private Text textNum;

    [SerializeField]
    private Image itemImage;

    public InventoryItem item;
    public InventoryManager manager;

    public void SetUp(InventoryItem newItem, InventoryManager newManager )
    {
        item = newItem;
        manager = newManager;

        if (item)
        {
            itemImage.sprite = item.itemImage;
            textNum.text = "" + item.quantity;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
