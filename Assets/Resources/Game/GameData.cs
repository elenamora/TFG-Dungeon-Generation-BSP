using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "My Game/ Game/ GameData")]
public class GameData : ScriptableObject
{

    public PlayerType playerType;
    public string username;
    public int level;

}
