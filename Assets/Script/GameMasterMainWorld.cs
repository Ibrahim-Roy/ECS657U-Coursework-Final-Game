using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMasterMainWorld : MonoBehaviour
{
    public Vector2 lastCheckpointPosition;
    public GameObject campfire;
    public bool reload = false;

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

    private static GameMasterMainWorld instance;
    private Player player;
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

    private void Awake() {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
            instance.player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            instance.player.setEquippedItem(instance.playerEquippedItemNumber);
            instance.player.setHealth(instance.playerHealth);
            instance.player.setHunger(instance.playerHunger);
            instance.player.setWood(instance.playerWood);
            instance.player.setStone(instance.playerStone);
            instance.player.setRawFish(instance.playerRawFish);
            instance.player.setRawMeat(instance.playerRawMeat);
            instance.player.setArrows(instance.playerArrows);
            instance.player.setFish(instance.playerFish);
            instance.player.setMeat(instance.playerMeat);
        }
        else
        {
            if(instance.reload)
            {
                instance.reload = false;
                Instantiate(campfire, instance.lastCheckpointPosition, Quaternion.identity);
            }
            instance.player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            instance.player.setEquippedItem(instance.playerEquippedItemNumber);
            instance.player.setHealth(instance.playerHealth);
            instance.player.setHunger(instance.playerHunger);
            instance.player.setWood(instance.playerWood);
            instance.player.setStone(instance.playerStone);
            instance.player.setRawFish(instance.playerRawFish);
            instance.player.setRawMeat(instance.playerRawMeat);
            instance.player.setArrows(instance.playerArrows);
            instance.player.setFish(instance.playerFish);
            instance.player.setMeat(instance.playerMeat);
            Destroy(this.gameObject);
        }
    }
}
