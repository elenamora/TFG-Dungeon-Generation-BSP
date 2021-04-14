using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "My Game/ Weapon Data")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public Sprite image;

    public int damage;
    public int range;

    public string description;

}
