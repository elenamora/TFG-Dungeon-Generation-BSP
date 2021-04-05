using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    public PowerUpData data;

    private Player player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject.GetComponent<Player>();

            player.currentHealth = player.IncreaseHealth(data.extraHealth);

            player.currentEnergy = player.IncreaseEnergy(data.extraEnergy);

            Destroy(gameObject);
        }
    }
}
