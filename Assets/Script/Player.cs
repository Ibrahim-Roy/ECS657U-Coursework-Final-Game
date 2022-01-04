using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Slider healthBar;
    public Slider hungerBar;

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
    }

    private void movementHandler()
    {
        rigidBody.velocity = new Vector2(horizontalInput, verticalInput) * movementSpeed;
    }

    private void useItem()
    {
        animator.SetTrigger("Use");
        if(equippedItemNumber == 1)
        {
             Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            ((transform.GetChild(0).gameObject).transform.GetChild(0).gameObject).GetComponent<RangedWeapon>().shoot(mouseWorldPosition, "HostileNPC");
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

}