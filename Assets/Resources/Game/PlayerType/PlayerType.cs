using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerType", menuName = "My Game/ Player/ PlayerType")]
public class PlayerType : ScriptableObject
{
    public int minWidth;
    public int maxWidth;

    public int minHeight;
    public int maxHeight;

    public int dungeonWidth;
    public int dungeonHeight;

    // Minum size of the room to only have between 0 and 1 enemies
    public int minRoomSizeEnemy;

    // Percentage of evil's level in the rooms (LOW, MEDIUM, HIGH) 
    [Header("% of Evil in Rooms")]
    public float lowPerc;
    public float mediumPerc;
    public float highPerc;

    // Quantity of enemies if the room is > minRoomSizeEnemy depending on its level of evil
    [Header("Number in Low Evil Rooms")]
    public int minLowEnemies;
    public int maxLowEnemies;

    [Header("Number in Medium Evil Rooms")]
    public int minMediumEnemies;
    public int maxMediumEnemies;

    [Header("Number in High Evil Rooms")]
    public int minHighEnemies;
    public int maxHighEnemies;

    public void getHeightWidth()
    {
        dungeonWidth = Random.Range(minWidth, maxWidth);
        dungeonHeight = Random.Range(minHeight, maxHeight);
    }
}
