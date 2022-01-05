using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Slider healthBar;
    public Slider hungerBar;

    public Text woodText;
    public Text stoneText;
    public Text rawFishText;
    public Text rawMeatText;

    public void collectResource(string resourceTag)
    {
        if(resourceTag == "Log")
        {
            incrementWood();
            setText(woodText, wood);
        }
        else if(resourceTag == "Stone")
        {
            incrementStone();
            setText(stoneText, stone);
        }
        else if(resourceTag == "Raw Fish")
        {
            incrementRawFish();
            setText(rawFishText, rawFish);
        }
        else if(resourceTag == "Raw Meat")
        {
            incrementRawMeat();
            setText(rawMeatText, rawMeat);
        }
    }

    public int craftArrow()
    {
        if(wood > 0 && stone > 0)
        {
            decrementWood();
            decrementStone();
            setText(stoneText, stone);
            setText(woodText, wood);
            incrementArrows();
        }
        return arrows;
    }

    private Rigidbody2D rigidBody;
    private Animator animator;
    private float movementSpeed = 2.0f;
    private float horizontalInput;
    private float verticalInput;
    private int equippedItemNumber = 0;
    private int maxHealth = 10;
    private int health;
    private int maxHunger = 10;
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
        health = maxHealth;
        hunger = maxHunger;
        healthBar.maxValue = maxHealth;
        hungerBar.maxValue = maxHunger;
        setHealth();
        setHunger();
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
    }

    private void inputHandler()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        if(Input.GetMouseButtonDown(0)) {
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
                decrementArrows();
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
                hunger--;
                setHunger();
            }
            else
            {
                health--;
                setHealth();
            }
        }
    }

    private void setHealth()
    {
        healthBar.value = health;
        healthBar.fillRect.GetComponentInChildren<Image>().color = Color.red;
    }

    private void setHunger()
    {
        hungerBar.value = hunger;
        hungerBar.fillRect.GetComponentInChildren<Image>().color = new Color(254,184,0,255);
    }

    private void incrementWood()
    {
        wood++;
    }

    private void incrementStone()
    {
        stone++;
    }

    private void incrementRawFish()
    {
        rawFish++;
    }

    private void incrementRawMeat()
    {
        rawMeat++;
    }

    private void incrementArrows()
    {
        arrows++;
    }

    private void decrementWood()
    {
        wood--;
    }

    private void decrementStone()
    {
        stone--;
    }

    private void decrementRawFish()
    {
        rawFish--;
    }

    private void decrementRawMeat()
    {
        rawMeat--;
    }

    private void decrementArrows()
    {
        arrows--;
    }

    private void setText(Text textbox, string text)
    {
        textbox.text = text;
    }

    private void setText(Text textbox, int text)
    {
        textbox.text = text.ToString();
    }
}
