using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "My Game/ Game/ GameData")]
public class GameData : ScriptableObject
{
    public List<DungeonData> dungeons;
    public List<EnemyManager> enemies;

    public GameData()
    {
        dungeons = new List<DungeonData>();
    }

    public void AddDungeon(DungeonData dungeonData)
    {
        dungeons.Add(dungeonData);   
    }

    public void AddEnemyManager(EnemyManager enemyManager)
    {
        enemies.Add(enemyManager);
    }

}
