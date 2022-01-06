using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public ParticleSystem dust;

    public void collectResource(string resourceTag)
    {
        if(resourceTag == "Log")
        {
            incrementWood(1);
            displayAlertOnHUD("Collected Wood");
        }
        else if(resourceTag == "Stone")
        {
            incrementStone(1);
            displayAlertOnHUD("Collected Stone");
        }
        else if(resourceTag == "Raw Fish")
        {
            incrementRawFish(1);
            displayAlertOnHUD("Collected Raw Fish");
        }
        else if(resourceTag == "Raw Meat")
        {
            incrementRawMeat(1);
            displayAlertOnHUD("Collected Raw Meat");
        }
    }

    public void craftArrow()
    {
        if(wood > 0 && stone > 0)
        {
            decrementWood(1);
            decrementStone(1);
            incrementArrows(1);
            displayAlertOnHUD("Arrow crafted successfully");
        }
        else
        {
            displayAlertOnHUD("Insufficient resources to craft an arrow");
        }
    }

    private Rigidbody2D rigidBody;
    private Animator animator;
    private GameObject HUD;
    private float movementSpeed = 2.0f;
    private float horizontalInput;
    private float verticalInput;
    private int equippedItemNumber = 0;
    private int maxHealth;
    private int health;
    private int maxHunger;
    private int hunger;
    private int wood = 0;
    private int stone = 0;
    private int rawFish = 0;
    private int rawMeat = 0;
    private int arrows = 0;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        HUD = GameObject.FindGameObjectWithTag("HUD");
        setMaxHealth(10);
        setMaxHunger(10);
        health = maxHealth;
        hunger = maxHunger;
    }

    private void Start()
    {
        StartCoroutine(decrementHunger(5.0f));
    }

    private void Update()
    {
        inputHandler();
        animationHandler();
    }

    private void FixedUpdate()
    {
        movementHandler();
    }

    private void animationHandler()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0;
        Vector3 mouseDirection = (mouseWorldPosition - transform.position).normalized;
        animator.SetFloat("Horizontal", mouseDirection.x);
        animator.SetFloat("Vertical", mouseDirection.y);
        animator.SetFloat("Speed", rigidBody.velocity.magnitude);
        if(rigidBody.velocity.magnitude > 0 && dust.isPlaying == false)
        {
            dust.Play();
        }
        else if(rigidBody.velocity.magnitude == 0 && dust.isPlaying == true)
        {
            dust.Stop();
        }
    }

    private void inputHandler()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) {
            useItem();
        }
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            changeEquippedItem(0);
        }
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            changeEquippedItem(1);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            changeEquippedItem(2);
        }
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            changeEquippedItem(3);
        }
        if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            changeEquippedItem(4);
        }
    }

    private void movementHandler()
    {
        rigidBody.velocity = new Vector2(horizontalInput, verticalInput) * movementSpeed;
    }

    private void useItem()
    {
        if(equippedItemNumber == 1)
        {
            if(arrows > 0)
            {
                animator.SetTrigger("Use");
                decrementArrows(1);
                Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                ((transform.GetChild(0).gameObject).transform.GetChild(0).gameObject).GetComponent<RangedWeapon>().shoot(mouseWorldPosition, "HostileNPC");
            }
        }
        else
        {
            animator.SetTrigger("Use");
        }
    }

    private void changeEquippedItem(int itemNumber)
    {
        equippedItemNumber = itemNumber;
        animator.SetInteger("Equipped Item Number", equippedItemNumber);
    }

    private IEnumerator decrementHunger(float time)
    {
        while(true)
        {
            yield return new WaitForSeconds(time);
            if (hunger > 0)
            {
                decrementHunger(1);
            }
            else
            {
                decrementHealth(1);
            }
        }
    }

    private void setMaxHealth(int amount)
    {
        maxHealth = amount;
        HUD.GetComponent<HUDManager>().setMaxHealthBarValue(maxHealth);
    }

    private void setMaxHunger(int amount)
    {
        maxHunger = amount;
        HUD.GetComponent<HUDManager>().setMaxHungerBarValue(maxHunger);
    }

    private void incrementHealth(int amount)
    {
        health += amount;
        HUD.GetComponent<HUDManager>().updateHUD("Health", health);
    }

    private void incrementHunger(int amount)
    {
        hunger += amount;
        HUD.GetComponent<HUDManager>().updateHUD("Hunger", hunger);
    }

    private void incrementWood(int amount)
    {
        wood += amount;
        HUD.GetComponent<HUDManager>().updateHUD("Wood", wood);
    }

    private void incrementStone(int amount)
    {
        stone += amount;
        HUD.GetComponent<HUDManager>().updateHUD("Stone", stone);
    }

    private void incrementRawFish(int amount)
    {
        rawFish += amount;
        HUD.GetComponent<HUDManager>().updateHUD("Raw Fish", rawFish);
    }

    private void incrementRawMeat(int amount)
    {
        rawMeat += amount;
        HUD.GetComponent<HUDManager>().updateHUD("Raw Meat", rawMeat);
    }

    private void incrementArrows(int amount)
    {
        arrows += amount;
        HUD.GetComponent<HUDManager>().updateHUD("Arrows", arrows);
    }

    private void decrementHealth(int amount)
    {
        health -= amount;
        HUD.GetComponent<HUDManager>().updateHUD("Health", health);
    }

    private void decrementHunger(int amount)
    {
        hunger -= amount;
        HUD.GetComponent<HUDManager>().updateHUD("Hunger", hunger);
    }

    private void decrementWood(int amount)
    {
        wood -= amount;
        HUD.GetComponent<HUDManager>().updateHUD("Wood", wood);
    }

    private void decrementStone(int amount)
    {
        stone -= amount;
        HUD.GetComponent<HUDManager>().updateHUD("Stone", stone);
    }

    private void decrementRawFish(int amount)
    {
        rawFish -= amount;
        HUD.GetComponent<HUDManager>().updateHUD("Raw Fish", rawFish);
    }

    private void decrementRawMeat(int amount)
    {
        rawMeat -= amount;
        HUD.GetComponent<HUDManager>().updateHUD("Raw Meat", rawMeat);
    }

    private void decrementArrows(int amount)
    {
        arrows -= amount;
        HUD.GetComponent<HUDManager>().updateHUD("Arrows", arrows);
    }

    private void displayAlertOnHUD(string text)
    {
        HUD.GetComponent<HUDManager>().alert(text);
    }
}
