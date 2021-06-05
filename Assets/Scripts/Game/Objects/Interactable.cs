using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    public bool playerInRange;

    /*
     * If the player is close enough the player will be in range and will be able to interact with the object 
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !collision.isTrigger)
        {
            playerInRange = true;
        }
    }

    /*
     * If the player is not close enough he won't be able to interact with the object
     */
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            playerInRange = false;
        }
    }
}
