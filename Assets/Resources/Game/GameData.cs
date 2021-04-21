using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "My Game/ Game/ GameData")]
public class GameData : ScriptableObject
{
    public DungeonData dungeon;

    public List<DungeonData> games;

    public GameData()
    {
        games = new List<DungeonData>();
    }

    public void AddGame(DungeonData dungeonData)
    {
        games.Add(dungeonData);   
    }

}
