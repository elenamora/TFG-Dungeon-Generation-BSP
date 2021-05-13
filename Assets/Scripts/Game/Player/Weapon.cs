using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] public WeaponData[] weaponData;
    public Player player;
    public GameObject weapon;

    public int damageWeapon;
    public int energyLoss;

    void Start()
    {
        player = FindObjectOfType<Player>();
        weapon = GameObject.FindGameObjectWithTag("Weapon");
        FindWeapon();
    }

    private void FindWeapon()
    {
        string name = weapon.name;
        switch (name)
        {
            case "sword":
                damageWeapon = weaponData[0].damage;
                energyLoss = weaponData[0].energy;
                break;
            case "axe":
                damageWeapon = weaponData[1].damage;
                energyLoss = weaponData[1].energy;
                break;
            case "hammer":
                damageWeapon = weaponData[2].damage;
                energyLoss = weaponData[2].energy;
                break;
        }  
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            player.currentEnergy -= energyLoss;
            player.energyBar.SetValue(player.currentEnergy);

            Enemy enemy;
            enemy = collision.gameObject.GetComponent<Enemy>();

            enemy.Hurt(damageWeapon);

            // The enemy will bounce back if we hit it
            Vector2 dif = collision.transform.position - transform.position;
            collision.transform.position = new Vector2(collision.transform.position.x + dif.x, collision.transform.position.y + dif.y); 
        }

        if (collision.gameObject.CompareTag("Breakable"))
        {
            collision.GetComponent<Breakable>().BreakItem();
        }
    }

}
