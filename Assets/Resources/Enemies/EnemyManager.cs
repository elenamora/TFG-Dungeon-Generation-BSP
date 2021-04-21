using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyManager", menuName = "My Game/ Enemy/ EnemyManager")]
public class EnemyManager : ScriptableObject
{
    // Minum size of the room to only have between 0 and 1 enemies
    public int minSize;

    // Percentage of evil's level in the rooms (LOW, MEDIUM, HIGH) 
    public float lowPerc;
    public float mediumPerc;
    public float highPerc;

    public int numOfEnemies;
    
    public List<int> initialEnemies;
    public List<int> killedEnemies;

    public EnemyManager()
    {
        minSize = 10;
        lowPerc = 0.2f;
        mediumPerc = 0.5f;
        highPerc = 0.3f;
        initialEnemies = new List<int>();
        killedEnemies = new List<int>();

    }

    public void ResetEnemies()
    {
        initialEnemies = new List<int>();
        killedEnemies = new List<int>();
    }

}
