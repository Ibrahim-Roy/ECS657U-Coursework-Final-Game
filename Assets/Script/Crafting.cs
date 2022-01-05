using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crafting : MonoBehaviour
{
    public Text textbox;
    private GameObject player;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");   
    }
    public void requestCrafting()
    {
        if(gameObject.tag == "Craft Arrow")
        {
            int newResourceAmount = player.GetComponent<Player>().craftArrow();
            if(newResourceAmount>0)
            {
                textbox.text = newResourceAmount.ToString();
            }
            else
            {
                Debug.Log("Unsufficient Amount of Resources!");
            }
        }
    }
}
