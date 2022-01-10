using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public ParticleSystem dust;
    public ParticleSystem blood;
    public GameObject campfire;

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
        if(wood >= 1 && stone >= 1)
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

    public void craftCampfire()
    {
        if(wood >=4 && stone >= 2)
        {
            decrementWood(4);
            decrementStone(4);
            Instantiate(campfire, transform.position, Quaternion.identity);
            displayAlertOnHUD("Campfire crafted successfully");
        }
        else
        {
            displayAlertOnHUD("Insufficient resources to craft a campfire");
        }
    }

    public void craftMeat()
    {
        if(nearCampfire)
        {
            if(rawMeat >= 1)
            {
                decrementRawMeat(1);
                incrementMeat(1);
                displayAlertOnHUD("Meat cooked successfully");
            }
            else
            {
                displayAlertOnHUD("You need raw meat to be able to cook it");
            }
        }
        else
        {
            displayAlertOnHUD("You need to be near a campfire to cook meat");
        }
    }

    public void craftFish()
    {
        if(nearCampfire)
        {
            if(rawFish >= 1)
            {
                decrementRawFish(1);
                incrementFish(1);
                displayAlertOnHUD("Fish cooked successfully");
            }
            else
            {
                displayAlertOnHUD("You need raw fish to be able to cook it");
            }
        }
        else
        {
            displayAlertOnHUD("You need to be near a campfire to cook fish");
        }
    }

    public void consumeFish()
    {
        if(fish >= 1)
        {
            decrementFish(1);
            incrementHealth(5);
            incrementHunger(maxHunger);
            displayAlertOnHUD("Consumed fish, +5 health and hunger is replenished");
        }
        else
        {
            displayAlertOnHUD("You have no cooked fish to eat");
        }
    }

    public void consumeMeat()
    {
        if(meat >= 1)
        {
            decrementMeat(1);
            incrementHealth(5);
            incrementHunger(maxHunger);
            displayAlertOnHUD("Consumed meat, +5 health and hunger is replenished");
        }
        else
        {
            displayAlertOnHUD("You have no cooked meat to eat");
        }
    }

    public void takeDamage(int amount)
    {
        decrementHealth(amount);
    }

    public void setCampfireStatus(bool status)
    {
        nearCampfire = status;
    }

    public void setEquippedItem(int itemNumber)
    {
        changeEquippedItem(itemNumber);
    }

    public void setHealth(int value)
    {
        health = value;
        HUD.updateHUD("Health", value);
    }

    public void setHunger(int value)
    {
        hunger = value;
        HUD.updateHUD("Hunger", value);
    }

    public void setWood(int value)
    {
        wood = value;
        HUD.updateHUD("Wood", value);
    }

    public void setStone(int value)
    {
        stone = value;
        HUD.updateHUD("Stone", value);
    }

    public void setRawFish(int value)
    {
        rawFish = value;
        HUD.updateHUD("Raw Fish", value);
    }

    public void setRawMeat(int value)
    {
        rawMeat = value;
        HUD.updateHUD("Raw Meat", value);
    }

    public void setArrows(int value)
    {
        arrows = value;
        HUD.updateHUD("Arrows", value);
    }

    public void setFish(int value)
    {
        fish = value;
        HUD.updateHUD("Fish", value);
    }

    public void setMeat(int value)
    {
        meat = value;
        HUD.updateHUD("Meat", value);
    }

    public int getEquippedItem()
    {
        return equippedItemNumber;
    }

    public int getHealth()
    {
        return health;
    }

    public int getHunger()
    {
        return hunger;
    }

    public int getWood()
    {
        return wood;
    }

    public int getStone()
    {
        return stone;
    }

    public int getRawFish()
    {
        return rawFish;
    }

    public int getRawMeat()
    {
        return rawMeat;
    }

    public int getArrows()
    {
        return arrows;
    }

    public int getFish()
    {
        return fish;
    }

    public int getMeat()
    {
        return meat;
    }

    private Rigidbody2D rigidBody;
    private Animator animator;
    private HUDManager HUD;
    private GameMasterMainWorld gameMaster;
    private EnumList.PlayMode currentMode;
    private float movementSpeed = 2.0f;
    private float horizontalInput;
    private float verticalInput;
    private int equippedItemNumber = 0;
    private bool shooting = false;
    private int maxHealth;
    private int health;
    private int maxHunger;
    private int hunger;
    private int wood = 0;
    private int stone = 0;
    private int rawFish = 0;
    private int rawMeat = 0;
    private int arrows = 0;
    private int fish = 0;
    private int meat = 0;
    private bool nearCampfire = false;

    private void Awake()
    {
        gameMaster = GameObject.FindGameObjectWithTag("Game Master").GetComponent<GameMasterMainWorld>();
        currentMode = gameMaster.getPlayMode();
        if (currentMode == EnumList.PlayMode.Main)//Do not apply this outside of main world
        {
            animator = GetComponent<Animator>();
            rigidBody = GetComponent<Rigidbody2D>();
        }
        HUD = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUDManager>();
        setMaxHealth(10);
        setMaxHunger(10);
        if(currentMode == EnumList.PlayMode.Main)
        {
            transform.position = gameMaster.getPlayerPosition();
            setEquippedItem(gameMaster.getEquippedItem());
            if(gameMaster.craftFireRequired())
            {
                Instantiate(campfire, transform.position, Quaternion.identity);
                gameMaster.updateCraftFire(false);
            }
        }
        setHealth(gameMaster.getHealth());
        setHunger(gameMaster.getHunger());
        setWood(gameMaster.getWood());
        setStone(gameMaster.getStone());
        setRawFish(gameMaster.getRawFish());
        setRawMeat(gameMaster.getRawMeat());
        setArrows(gameMaster.getArrows());
        setFish(gameMaster.getFish());
        setMeat(gameMaster.getMeat());
    }

    private void Start()
    {
        StartCoroutine(decrementHunger(5.0f));
    }

    private void Update()
    {
        if (currentMode == EnumList.PlayMode.Main)//Do not apply outside of main world
        {
            inputHandler();
            animationHandler();
        }
    }

    private void FixedUpdate()
    {
        if (currentMode == EnumList.PlayMode.Main)//Do not apply outside of main world
        {
            movementHandler();
        }
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
        else if(Input.GetKeyDown(KeyCode.Tab))
        {
            changeEquippedItem(0);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            changeEquippedItem(1);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            changeEquippedItem(2);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            changeEquippedItem(3);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha4))
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
            if(arrows > 0 && !shooting)
            {
                shooting = true;
                animator.SetTrigger("Use");
                Invoke("useBow", 0.12f);
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
        HUD.setMaxHealthBarValue(maxHealth);
    }

    private void setMaxHunger(int amount)
    {
        maxHunger = amount;
        HUD.setMaxHungerBarValue(maxHunger);
    }

    private void incrementHealth(int amount)
    {
        if(health < maxHealth)
        {
            health += amount;
            if(health > maxHealth)
            {
                health = maxHealth;
            }
        }
        HUD.updateHUD("Health", health);
    }

    private void incrementHunger(int amount)
    {
        if(hunger < maxHunger)
        {
            hunger += amount;
            if(hunger > maxHunger)
            {
                hunger = maxHunger;
            }
        }
        HUD.updateHUD("Hunger", hunger);
    }

    private void incrementWood(int amount)
    {
        wood += amount;
        HUD.updateHUD("Wood", wood);
    }

    private void incrementStone(int amount)
    {
        stone += amount;
        HUD.updateHUD("Stone", stone);
    }

    private void incrementRawFish(int amount)
    {
        rawFish += amount;
        HUD.updateHUD("Raw Fish", rawFish);
    }

    private void incrementRawMeat(int amount)
    {
        rawMeat += amount;
        HUD.updateHUD("Raw Meat", rawMeat);
    }

    private void incrementArrows(int amount)
    {
        arrows += amount;
        HUD.updateHUD("Arrows", arrows);
    }

    private void incrementMeat(int amount)
    {
        meat += amount;
        HUD.updateHUD("Meat", meat);
    }

    private void incrementFish(int amount)
    {
        fish += amount;
        HUD.updateHUD("Fish", fish);
    }

    private void decrementHealth(int amount)
    {
        health -= amount;
        if(!blood.isPlaying && currentMode == EnumList.PlayMode.Main)
        {
            blood.Play();
        }
        HUD.updateHUD("Health", health);
        if(health <= 0)
        {
            die();
        }
    }

    private void decrementHunger(int amount)
    {
        hunger -= amount;
        HUD.updateHUD("Hunger", hunger);
    }

    private void decrementWood(int amount)
    {
        wood -= amount;
        HUD.updateHUD("Wood", wood);
    }

    private void decrementStone(int amount)
    {
        stone -= amount;
        HUD.updateHUD("Stone", stone);
    }

    private void decrementRawFish(int amount)
    {
        rawFish -= amount;
        HUD.updateHUD("Raw Fish", rawFish);
    }

    private void decrementRawMeat(int amount)
    {
        rawMeat -= amount;
        HUD.updateHUD("Raw Meat", rawMeat);
    }

    private void decrementArrows(int amount)
    {
        arrows -= amount;
        HUD.updateHUD("Arrows", arrows);
    }

    private void decrementMeat(int amount)
    {
        meat -= amount;
        HUD.updateHUD("Meat", meat);
    }

    private void decrementFish(int amount)
    {
        fish -= amount;
        HUD.updateHUD("Fish", fish);
    }

    private void displayAlertOnHUD(string text)
    {
        HUD.alert(text);    
    }

    private void useBow()
    {
        decrementArrows(1);
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ((transform.GetChild(0).gameObject).transform.GetChild(0).gameObject).GetComponent<RangedWeapon>().shoot(mouseWorldPosition, "HostileNPC");
        shooting = false;
    }

    private void die(){
        gameMaster.updateCraftFire(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
