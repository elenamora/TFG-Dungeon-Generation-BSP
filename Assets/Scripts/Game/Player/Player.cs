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

    /*** HEALTH VARIABLES ***/
    [Header("Health")]
    public int maxHealth = 50;
    public int currentHealth;
    public StatsBar healthBar;

    /*** ENERGY VARIABLES ***/
    [Header("Energy")]
    public int maxEnergy = 50;
    public int currentEnergy;
    public StatsBar energyBar;

    private StatsBar[] bars;


    /*** HURT FLASH ***/
    private bool flash;
    private float flashTime = 1f;
    private float flashCount = 0f;
    private SpriteRenderer playerSprite;
    private SpriteRenderer weaponSprite;

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

        foreach (StatsBar bar in bars)
        {
            if (bar.CompareTag("Health")) { healthBar = bar; }
            else if (bar.CompareTag("Energy")) { energyBar = bar; }
        }

        currentHealth = maxHealth;
        healthBar.SetMax(maxHealth);


        currentEnergy = maxEnergy;
        energyBar.SetMax(maxEnergy);

        playerSprite = GetComponent<SpriteRenderer>();
        attackCount = attackTime;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Attack();
        HurtFlash();

        UseItems();
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

        if (currentHealth <= 0) { SceneManager.LoadScene(SceneManager.GetActiveScene().name); }
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
        if ((currentHealth + health) <= maxHealth) { currentHealth += health; }

        else if (currentHealth + health > maxHealth) { currentHealth = maxHealth; }

        healthBar.SetValue(currentHealth);

        return currentHealth;

    }

    public int IncreaseEnergy(int energy)
    {
        if ((currentEnergy + energy) <= maxEnergy) { currentEnergy += energy; }

        else if (currentEnergy + energy > maxEnergy) { currentEnergy = maxEnergy; }

        energyBar.SetValue(currentEnergy);

        return currentEnergy;
    }

    public void UseItems()
    {
        if (Input.GetKeyDown(KeyCode.R))
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

        if (Input.GetKeyDown(KeyCode.E))
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

}
