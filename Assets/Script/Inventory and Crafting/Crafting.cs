using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    public void requestCrafting()
    {
        if(gameObject.tag == "Craft Arrow")
        {
            player.GetComponent<Player>().craftArrow();
        }
        else if(gameObject.tag == "Craft Campfire")
        {
            player.GetComponent<Player>().craftCampfire();
        }
        else if(gameObject.tag == "Craft Meat")
        {
            player.GetComponent<Player>().craftMeat();
        }
        else if(gameObject.tag == "Craft Fish")
        {
            player.GetComponent<Player>().craftFish();
        }
    }

    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
}
