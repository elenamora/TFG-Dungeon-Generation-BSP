using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChestData", menuName = "My Game/ Chest Data")]
public class ChestData : ScriptableObject
{
    public string chestName;
    public string chestDescription;

    public string itemToOpen;
    public int quantityOfItemToOpen;

    public string info;

}
