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
