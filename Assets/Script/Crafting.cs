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
    }

    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
}
