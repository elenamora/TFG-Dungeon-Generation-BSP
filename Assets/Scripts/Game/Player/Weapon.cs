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

    /*
     * This function will assign values to the damage the weapon can have and the energy loss that the player will suffer
     * when attacking.
     * The values will depend on the weapon choosen by the player on the menu before starting the game
     */
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

    /*
     * Method that will be activated when the weapon collides with the enemy and any object of type breakable (like pots)
     * The weapon acts like an extension of the player.
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the weapon collides with the enemy besides hurting the enemy the player will loss part of his energy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            player.currentEnergy -= energyLoss;
            player.energyBar.SetValue(player.currentEnergy);

            Enemy enemy;
            enemy = collision.gameObject.GetComponent<Enemy>();

            enemy.Hurt(damageWeapon);

            // The enemy will bounce back if we hit it
            Vector2 dif = (collision.transform.position - transform.position)*2;
            collision.transform.position = new Vector2(collision.transform.position.x + dif.x, collision.transform.position.y + dif.y); 
        }

        // If the weapon collides with a breakable object the function BreakItem will be called
        if (collision.gameObject.CompareTag("Breakable"))
        {
            collision.GetComponent<Breakable>().BreakItem();
        }
    }

}
