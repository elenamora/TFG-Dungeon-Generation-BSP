﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponData weaponData;

    public int damageWeapon;

    public void Start()
    {
        damageWeapon = weaponData.damage;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy;
            enemy = collision.gameObject.GetComponent<Enemy>();

            enemy.Hurt(damageWeapon);

            // The enemy will bounce back if we hit it
            Vector2 dif = collision.transform.position - transform.position ;
            collision.transform.position = new Vector2(collision.transform.position.x + dif.x, collision.transform.position.y + dif.y);
        }
    }

}
