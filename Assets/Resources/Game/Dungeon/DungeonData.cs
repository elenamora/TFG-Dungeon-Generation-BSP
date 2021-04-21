using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DungeonData", menuName = "My Game/ Game/ DungeonData")]
public class DungeonData : ScriptableObject
{
    public int dungeonWidth;
    public int dungeonHeight;
    public int minSizeRoom;

    public List<Rect> rooms;
    public List<Rect> hallways;

    public DungeonData()
    {
        dungeonWidth = 50;
        dungeonHeight = 30;
        minSizeRoom = 6;
    }

    public void ResetDungeon()
    {
        rooms = new List<Rect>();
        hallways = new List<Rect>();
    }
}
