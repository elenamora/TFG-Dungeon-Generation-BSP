using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameState", menuName = "My Game/ Game/ Game State")]
public class GameState : ScriptableObject
{
    public bool win;
    public bool loose;

    public GameState()
    {
        win = false;
        loose = false;
    }

}
