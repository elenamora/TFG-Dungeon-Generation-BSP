using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="EnemyData", menuName = "My Game/ Enemy /Enemy Data")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public GameObject enemyModel;

    public int health;
    public int damage;

    public float minDist;
    public float attackSpeed;
    public float patrolSpeed;
}
