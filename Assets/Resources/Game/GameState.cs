using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameState", menuName = "My Game/ Game/ Game State")]
public class GameState : ScriptableObject
{
    public int level;

    public bool win;
    public bool loose;

    public GameState()
    {
        level = 1;
        win = false;
        loose = false;
    }

    public void LevelUp()
    {
        level += 1;
    }
}
