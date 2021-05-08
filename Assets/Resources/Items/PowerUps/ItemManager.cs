using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemManager", menuName = "My Game/ Items/ ItemManager")]
public class ItemManager : ScriptableObject
{
    public int initialItems;
    public int pickedItems;

    public ItemManager()
    {
        initialItems = 0;
        pickedItems = 0;
    }

    public void ResetItems()
    {
        initialItems = 0;
        pickedItems = 0;
    }
}
