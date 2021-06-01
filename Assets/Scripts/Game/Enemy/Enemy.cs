using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public EnemyManager enemyManager;

    private float range, speed;

    private Transform playerTransform;
    private Animator animator;
    private GameObject homePos;

    public EnemyData data;

    /*** ATTACK VARIABLES ***/
    private float waitToHurt = 2f;
    private bool isTouching = false;

    private Player player;

    /*** HEALTH VARIABLES ***/
    public int currentHealth;

    /*** HURT FLASH ***/
    private bool flash;
    private float flashTime = 1f;
    private float flashCount = 0f;
    private SpriteRenderer enemySprite;

    /*** DIE VARIABLES ***/
    public LootTable lootTable;

    /*** PATROL VARIABLES ***/
    // A minimum and maximum time delay for taking a decision, choosing a direction to move in
    private Vector2 decisionTime = new Vector2(1, 2);
    private float decisionTimeCount = 0;

    // The possible directions the enemy can move to: right, left, up and down
    private Vector3[] moveDirections = new Vector3[] { Vector3.right, Vector3.left, Vector3.up, Vector3.down };
    private int currentMoveDirection;


    // Start is called before the first frame update
    void Start()
    {
        homePos = new GameObject();
        animator = GetComponent<Animator>();
        homePos = Instantiate(homePos, new Vector3(transform.position.x, transform.position.y, 0f), Quaternion.identity);

        // Set a random time delay for taking a decision
        decisionTimeCount = Random.Range(decisionTime.x, decisionTime.y);

        player = FindObjectOfType<Player>();

        currentHealth = data.health;
        enemySprite = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        range = Vector2.Distance(transform.position, playerTransform.position);
        Move();
        HurtFlash();

        if (isTouching)
        {
            waitToHurt -= Time.deltaTime;
            if (waitToHurt <= 0)
            {
                player.Hurt(data.damage);
                waitToHurt = 2f;
            }
        }
    }

    /*
     * Defines the movement of the enemy
     * If the player is close enough, the enemy will change its speed and direction to attack the player.
     * If it is not, the Patrol function will be called.
     */
    public void Move()
    {
        Patrol();

        if (range < data.minDist)
        {
            speed = data.attackSpeed;
            animator.SetBool("isMoving", true);
            animator.SetFloat("moveX", playerTransform.position.x - transform.position.x);
            animator.SetFloat("moveY", playerTransform.position.y - transform.position.y);

            transform.position = Vector3.MoveTowards(transform.position, playerTransform.transform.position, speed * Time.deltaTime); 
        }
    }

    /*
     * Defines the movement of the enemy while the player is not close.
     */
    public void Patrol()
    {
        speed = data.patrolSpeed;
        transform.position += moveDirections[currentMoveDirection] * Time.deltaTime * speed;

        transform.LookAt(transform.position);

        animator.SetBool("isMoving", true);
        animator.SetFloat("moveX", moveDirections[currentMoveDirection].x);
        animator.SetFloat("moveY", moveDirections[currentMoveDirection].y);


        if (decisionTimeCount > 0) decisionTimeCount -= Time.deltaTime;
        else
        {
            // Choose a random time delay for taking a decision ( changing direction, or standing in place for a while )
            decisionTimeCount = Random.Range(decisionTime.x, decisionTime.y);

            // Choose whether to move sideways or up/down
            currentMoveDirection = Mathf.FloorToInt(Random.Range(0, moveDirections.Length));
        }
    }

    /*
     * Method that will subtract life from the enemy when he is attacked by the player.
     */
    public void Hurt(int damage)
    {
        currentHealth -= damage;
        //healthBar.SetHealth(currentLife);
        flash = true;
        flashCount = flashTime;

        // If the enemy dies a number (0 for BAT, 1 for SKELETON and 2 for BLACK) will be added to the list of killed
        // enemies that has the enemyManager. That way we can keep track of how many enemies the player has killed and
        // what kind they are.
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
            if(data.enemyName == "Bat") { enemyManager.killedEnemies.Add(0); }
            else if(data.enemyName == "Skeleton") { enemyManager.killedEnemies.Add(1); }
            else if(data.enemyName == "Black") { enemyManager.killedEnemies.Add(2); }
            
            MakeLoot();    
        }
    }

    /*
     * Method called when the enemy is hurt.
     * Enemy sprite will flash for a second to indicate visually that he has been hurt.
     */
    private void HurtFlash()
    {
        // We change the alpha channel every 6th of a second to create the flash 
        if (flash)
        {
            if (flashCount > flashTime * .99f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 0f);
            }
            else if (flashCount > flashTime * .82f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 1f);
            }
            else if (flashCount > flashTime * .66f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 0f);
            }
            else if (flashCount > flashTime * .49f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 1f);
            }
            else if (flashCount > flashTime * .33f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 0f);
            }
            else if (flashCount > flashTime * .16f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 1f);
            }
            else if (flashCount > 0)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 0f);
            }
            else
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 1f);
                flash = false;
            }
            flashCount -= Time.deltaTime;
        }
    }

    /*
     * Function that will instantiate a powerup depending on the loot Table of each enemy.
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

    /*
     * Method that is activated when the enemy collides with any other GameObject from the game.
     */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the enemy collides with the Player, the Hurt() method of the player will be called and the Player's health
        // will decrease.
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().Hurt(data.damage);
        }

        // If the enemy collides with the wall he will change its direction, without waiting for the patrol method to change it
        // It will check in which direction is was moving when colliding so its more probable that the enemy won't get stuck.
        if (collision.gameObject.CompareTag("Tile"))
        {
            if (currentMoveDirection == 0)
            {
                currentMoveDirection = Mathf.FloorToInt(Random.Range(1, moveDirections.Length));
            }
            else if (currentMoveDirection == 1)
            {
                List<int> temp = new List<int>() { 0, 2, 3 };
                currentMoveDirection = temp[Random.Range(0, temp.Count)];
            }

            else if (currentMoveDirection == 2)
            {
                List<int> temp2 = new List<int>() { 0, 1, 3 };
                currentMoveDirection = temp2[Random.Range(0, temp2.Count)];
            }

            else if (currentMoveDirection == 3)
            {
                currentMoveDirection = Mathf.FloorToInt(Random.Range(0, moveDirections.Length - 1));
            }
            
            Patrol();
        }
    }

    /*
     * If the enemy keeps touching the player after colliding with him on the Update method we'll wait some time before 
     * the enemy is able to hurt the player again.
     */
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) { isTouching = true; }  
    }

    /*
     * If the player moves away the enemy and the player won't be touching anymore and we'll reset the time the enemy
     * has to wait to hurt the player.
     */
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isTouching = false;
            waitToHurt = 2f;
        }
    }

}
