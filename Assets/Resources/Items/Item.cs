using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "My Game/ Item")]
public class Item : ScriptableObject
{
    public Sprite itemSprite;

    public string itemName;
    public string itemDescription;
}
