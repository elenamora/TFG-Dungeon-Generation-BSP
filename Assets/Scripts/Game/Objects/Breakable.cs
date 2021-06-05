using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    private Animator animator;

    public LootTable lootTable;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    /*
     * If the player breaks an item the breaking animation will start and the MakeLoot function will be called
     */
    public void BreakItem()
    {
        animator.SetBool("isBroken", true);
        StartCoroutine(BreakCo());    
    }

    IEnumerator BreakCo()
    {
        yield return new WaitForSeconds(.3f);
        gameObject.SetActive(false);
        MakeLoot();
    }

    /*
     * It will instantiate a random powerup on the item position given the LootTable of the breakable object
     */
    public void MakeLoot()
    {
        if (lootTable != null)
        {
            PowerUp pwup = lootTable.LootPowerUp();

            if (pwup != null)
            {
                Instantiate(pwup, transform.position, Quaternion.identity);
            }
        }
    }
}
