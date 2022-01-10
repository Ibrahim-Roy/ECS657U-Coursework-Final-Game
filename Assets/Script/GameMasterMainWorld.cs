using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMasterMainWorld : MonoBehaviour
{
    public GameObject campfire;

    public void setPlayMode(EnumList.PlayMode mode)
    {
        currentMode = mode;
    }

    public void setPlayerPosition(Vector2 position)
    {
        playerPosition = position;
    }

    public void setPlayerDestinationPosition(Vector2 position)
    {
        playerDestinationPosition = position;
    }

    public void setEquippedItem(int itemNumber)
    {
        playerEquippedItemNumber = itemNumber;
    }

    public void setHealth(int value)
    {
        playerHealth = value;
    }

    public void setHunger(int value)
    {
        playerHunger = value;
    }

    public void setWood(int value)
    {
        playerWood = value;
    }

    public void setStone(int value)
    {
        playerStone = value;
    }

    public void setRawFish(int value)
    {
        playerRawFish = value;
    }

    public void setRawMeat(int value)
    {
        playerRawMeat = value;
    }

    public void setArrows(int value)
    {
        playerArrows = value;
    }

    public void setFish(int value)
    {
        playerFish = value;
    }

    public void setMeat(int value)
    {
        playerMeat = value;
    }

    public void updateCraftFire(bool status)
    {
        craftFire = status;
    }

    public EnumList.PlayMode getPlayMode()
    {
        return currentMode;
    }

    public Vector2 getPlayerPosition()
    {
        return playerPosition;
    }

    public int getEquippedItem()
    {
        return playerEquippedItemNumber;
    }

    public int getHealth()
    {
        return playerHealth;
    }

    public int getHunger()
    {
        return playerHunger;
    }

    public int getWood()
    {
        return playerWood;
    }

    public int getStone()
    {
        return playerStone;
    }

    public int getRawFish()
    {
        return playerRawFish;
    }

    public int getRawMeat()
    {
        return playerRawMeat;
    }

    public int getArrows()
    {
        return playerArrows;
    }

    public int getFish()
    {
        return playerFish;
    }

    public int getMeat()
    {
        return playerMeat;
    }

    public bool craftFireRequired()
    {
        return craftFire;
    }

    private static GameMasterMainWorld instance;
    [SerializeField] private EnumList.PlayMode currentMode;
    [SerializeField] private Vector2 playerPosition;
    private Vector2 playerDestinationPosition;
    private int playerEquippedItemNumber = 0;
    private int playerHealth = 10;
    private int playerHunger = 10;
    private int playerWood = 0;
    private int playerStone = 0;
    private int playerRawFish = 0;
    private int playerRawMeat = 0;
    private int playerArrows = 0;
    private int playerFish = 0;
    private int playerMeat = 0;
    private bool craftFire = false;

    private void Awake() {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
