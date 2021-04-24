using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "My Game/ Game/ GameData")]
public class GameData : ScriptableObject
{
    public List<DungeonData> dungeons;
    public List<EnemyManager> enemies;
    public List<ItemManager> items;

    public PlayerType playerType;

    public GameData()
    {
        dungeons = new List<DungeonData>();
        enemies = new List<EnemyManager>();
        items = new List<ItemManager>();
    }

    public void AddDungeon(DungeonData dungeonData)
    {
        dungeons.Add(dungeonData);   
    }

    public void AddEnemyManager(EnemyManager enemyManager)
    {
        enemies.Add(enemyManager);
    }

    public void AddItemManager(ItemManager itemManager)
    {
        items.Add(itemManager);
    }

}
