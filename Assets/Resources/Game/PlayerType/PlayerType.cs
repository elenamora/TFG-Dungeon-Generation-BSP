using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerType", menuName = "My Game/ Player/ PlayerType")]
public class PlayerType : ScriptableObject
{
    public int dungeonWidth;
    public int dungeonHeight;

    // Minum size of the room to only have between 0 and 1 enemies
    public int minRoomSizeEnemy;

    // Percentage of evil's level in the rooms (LOW, MEDIUM, HIGH) 
    public float lowPerc;
    public float mediumPerc;
    public float highPerc;

    // Quantity range of enemies if the room is > minRoomSizeEnemy depending on its level of evil
    public int[,] lowEnemies;
    public int[,] mediumEnemies;
    public int[,] highEnemies;
}
