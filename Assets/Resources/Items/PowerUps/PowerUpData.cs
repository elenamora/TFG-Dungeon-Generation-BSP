using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PowerUpData", menuName = "My Game/ PowerUp Data")]
public class PowerUpData : ScriptableObject
{
    public string poweUpName;
    public string powerUpDescription;

    public int extraHealth;
    public int extraEnergy;
    public int extraMoney;
}
