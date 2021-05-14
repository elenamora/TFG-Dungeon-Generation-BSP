using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "My Game/ Player/ PlayerData")]
public class PlayerData : ScriptableObject
{

    public string playerName;

    public Sprite playerImage;

    public int health;
    public int energy;
    
}
