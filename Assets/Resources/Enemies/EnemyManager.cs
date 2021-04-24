using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyManager", menuName = "My Game/ Enemy/ EnemyManager")]
public class EnemyManager : ScriptableObject
{    
    public List<int> initialEnemies;
    public List<int> killedEnemies;

    public EnemyManager()
    {
        initialEnemies = new List<int>();
        killedEnemies = new List<int>();

    }

    public void ResetEnemies()
    {
        initialEnemies = new List<int>();
        killedEnemies = new List<int>();
    }

}
