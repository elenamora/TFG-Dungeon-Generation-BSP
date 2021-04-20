using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps : MonoBehaviour
{ 
    public ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(ps, transform.position, transform.rotation);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().Hurt(5);
        }
    }


}
