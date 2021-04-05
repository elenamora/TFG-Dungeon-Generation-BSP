using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    private float range, speed;

    private Transform playerTransform;
    private Animator animator;
    private GameObject homePos;

    private bool playerCollision = false;

    public EnemyData data;

    /*** HEALTH VARIABLES ***/
    public int currentHealth;
    //private HealthBar healthBar;

    /*** HURT FLASH ***/
    private bool flash;
    private float flashTime = 1f;
    private float flashCount = 0f;
    private SpriteRenderer enemySprite;

    /*** ATTACK VARIABLES ***/
    private float waitToHurt = 2f;
    private bool isTouching = false;

    private Player player;

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


    public void Move()
    {
        Patrol();

        if (range < data.minDist)
        {
            //if (!playerCollision)
            //{
                speed = data.attackSpeed;
                animator.SetBool("isMoving", true);
                animator.SetFloat("moveX", playerTransform.position.x - transform.position.x);
                animator.SetFloat("moveY", playerTransform.position.y - transform.position.y);

                transform.position = Vector3.MoveTowards(transform.position, playerTransform.transform.position, speed * Time.deltaTime);
            //}
        }
    }

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

    public void Hurt(int damage)
    {
        currentHealth -= damage;
        //healthBar.SetHealth(currentLife);
        flash = true;
        flashCount = flashTime;

        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
            MakeLoot();    
        }
    }

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //collision.gameObject.SetActive(false);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            collision.gameObject.GetComponent<Player>().Hurt(data.damage);

            // The player will bounce back if we hit it
            Vector2 dif = collision.transform.position - transform.position;
            collision.transform.position = new Vector2(collision.transform.position.x + dif.x, collision.transform.position.y + dif.y);

        }

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

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) { isTouching = true; }  
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isTouching = false;
            waitToHurt = 2f;
        }
    }

}
