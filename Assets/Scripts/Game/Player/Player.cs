using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    private float speed = 4.0f;
    Rigidbody2D rb;
    private Animator animator;

    public Inventory inventory;

    public PlayerData data;

    /*** HEALTH VARIABLES ***/
    [Header("Health")]
    public int currentHealth;
    public StatsBar healthBar;

    private bool isAlive;

    public GameState gameState;
    public GameEvent looseEvent;

    /*** ENERGY VARIABLES ***/
    [Header("Energy")]
    public int currentEnergy;
    public StatsBar energyBar;

    private StatsBar[] bars;


    /*** HURT FLASH ***/
    private bool flash;
    private float flashTime = 1f;
    private float flashCount = 0f;
    private SpriteRenderer playerSprite;
    //private SpriteRenderer weaponSprite;

    /*** ATTACK VARIABLES ***/
    private bool attacking;
    private float attackTime = .2f;
    private float attackCount;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bars = FindObjectsOfType<StatsBar>();
        //weaponSprite = GameObject.FindGameObjectWithTag("Weapon").GetComponent<SpriteRenderer>();

        foreach (StatsBar bar in bars)
        {
            if (bar.CompareTag("Health")) { healthBar = bar; }
            else if (bar.CompareTag("Energy")) { energyBar = bar; }
        }

        currentHealth = data.health;
        healthBar.SetMax(data.health);


        currentEnergy = data.energy;
        energyBar.SetMax(data.energy);

        playerSprite = GetComponent<SpriteRenderer>();
        attackCount = attackTime;

        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Attack();
        HurtFlash();

        UseItems();

        if(isAlive) isDead();
    }

    void Move()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        // If we move in diagonal we will have half the speed so we always have the same speed
        if(vertical != 0 && horizontal != 0) { speed /= 2; }
        else { speed = 4f; }

        rb.velocity = new Vector2(horizontal * speed, vertical * speed);

        animator.SetFloat("moveX", rb.velocity.x);
        animator.SetFloat("moveY", rb.velocity.y);

        if (horizontal == 1 || horizontal == -1 || vertical == 1 || vertical == -1)
        {
            animator.SetFloat("lastMoveX", horizontal);
            animator.SetFloat("lastMoveY", vertical);
        }
    }

    void Attack()
    {
        if (attacking)
        {
            attackCount -= Time.deltaTime;
            rb.velocity = Vector2.zero;
            if (attackCount <= 0)
            {
                animator.SetBool("isAttacking", false);
                attacking = false;
                attackCount = attackTime;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentEnergy > 0)
            {
                rb.velocity = Vector2.zero;
                attackCount = attackTime;
                attacking = true;
                animator.SetBool("isAttacking", true);
            }
            else
            {
                attacking = false;
                animator.SetBool("isAttacking", false);
            }
        }
    }

    public void Hurt(int damage)
    {
        currentHealth -= damage;
        healthBar.SetValue(currentHealth);
        flash = true;
        flashCount = flashTime;
    }

    private void HurtFlash()
    {
        // We change the alpha channel every 6th of a second to create the flash 
        if (flash)
        {
            if (flashCount > flashTime * .99f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0f);
                //weaponSprite.color = new Color(weaponSprite.color.r, weaponSprite.color.g, weaponSprite.color.b, 0f);
            }
            else if (flashCount > flashTime * .82f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
                //weaponSprite.color = new Color(weaponSprite.color.r, weaponSprite.color.g, weaponSprite.color.b, 1f);
            }
            else if (flashCount > flashTime * .66f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0f);
                //weaponSprite.color = new Color(weaponSprite.color.r, weaponSprite.color.g, weaponSprite.color.b, 0f);
            }
            else if (flashCount > flashTime * .49f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
                //weaponSprite.color = new Color(weaponSprite.color.r, weaponSprite.color.g, weaponSprite.color.b, 1f);
            }
            else if (flashCount > flashTime * .33f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0f);
                //weaponSprite.color = new Color(weaponSprite.color.r, weaponSprite.color.g, weaponSprite.color.b, 0f);
            }
            else if (flashCount > flashTime * .16f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
                //weaponSprite.color = new Color(weaponSprite.color.r, weaponSprite.color.g, weaponSprite.color.b, 1f);
            }
            else if (flashCount > 0)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0f);
                //weaponSprite.color = new Color(weaponSprite.color.r, weaponSprite.color.g, weaponSprite.color.b, 0f);
            }
            else
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
                //weaponSprite.color = new Color(weaponSprite.color.r, weaponSprite.color.g, weaponSprite.color.b, 1f);
                flash = false;
            }
            flashCount -= Time.deltaTime;
        }
    }

    public int IncreaseHealth(int health)
    {
        if ((currentHealth + health) <= data.health) { currentHealth += health; }

        else if (currentHealth + health > data.health) { currentHealth = data.health; }

        healthBar.SetValue(currentHealth);

        return currentHealth;

    }

    public int IncreaseEnergy(int energy)
    {
        if ((currentEnergy + energy) <= data.energy) { currentEnergy += energy; }

        else if (currentEnergy + energy > data.energy) { currentEnergy = data.energy; }

        energyBar.SetValue(currentEnergy);

        return currentEnergy;
    }

    public void UseItems()
    {
        if (Input.GetKeyDown(KeyCode.R) && currentHealth < data.health)
        {
            foreach (InventoryItem item in inventory.inventoryItems)
            {
                if (item.itemName == "Health Potion" && item.quantity > 0)
                {
                    currentHealth = IncreaseHealth(item.data.extraHealth);
                    item.quantity -= 1;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && currentEnergy < data.energy)
        {
            foreach (InventoryItem item in inventory.inventoryItems)
            {
                if (item.itemName == "Energy Potion" && item.quantity > 0)
                {
                    currentEnergy = IncreaseEnergy(item.data.extraEnergy);
                    item.quantity -= 1;
                }
            }
            
        }
            
    }

    public void isDead()
    {
        if(currentHealth <= 0)
        {
            isAlive = false;
            gameState.loose = true;
            looseEvent.Raise();
        }
    }

}
