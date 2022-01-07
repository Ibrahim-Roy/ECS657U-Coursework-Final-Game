using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftFish : MonoBehaviour
{

    Rigidbody2D leftFish_Rigidbody;

    private GameObject player;
    private int damage = 0;
    private int foodValue = 0;

    void Start()
    {
        leftFish_Rigidbody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Sky");

        fishDefine(); //Predetermine fish life

    }

    void move(float u)
    {
        leftFish_Rigidbody.velocity = new Vector2(u, 0);
    }
    
    void stopMove()
    {
        leftFish_Rigidbody.velocity = new Vector2(0, 0);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "hook")//hooked fish
        {
            stopMove();
            this.transform.parent = col.transform;      
            leftFish_Rigidbody.velocity = new Vector2(0, 4);
        }

        if (col.gameObject.tag == "Sky")//Collect resource
        {
            if (gameObject.tag == "Raw Fish")
            {
                for (int i = 0; i < foodValue; i++)
                player.GetComponent<Player>().collectResource("Raw Fish");
            }

            else if (gameObject.tag == "Bad Fish")
            {
                player.GetComponent<Player>().takeDamage(damage);
            }
            Destroy(gameObject);//Add sound here
        }

        if (col.gameObject.tag == "Spawner Right")//Despawn once offscreen
        {
            Destroy(gameObject);
        }
    }

    private void fishDefine()
    {
        if (gameObject.tag == "Raw Fish")
        {
            if (gameObject.name == "Fish(Clone)")
                defineNormalFish();
            else if (gameObject.name == "Nemo(Clone)")
            {
                defineNemo();
            }
            else if (gameObject.name == "Colourful(Clone)")
            {
                Debug.Log("Colourful Spawned");
                defineColourful();
            }
            else if (gameObject.name == "Golden(Clone)")
                defineGolden();
        }
        else if (gameObject.tag == "Bad Fish")
        {
            if (gameObject.name == "Bad Fish(Clone)")
                defineBadFish();
            else if (gameObject.name == "Swordfish(Clone)")
                defineSword();
            else if (gameObject.name == "Mine(Clone)")
                defineMine();
        }
    }

    //Size, speed and food values of food fish
    private void defineNormalFish()
    {
        transform.localScale = RandomScale2D(1f, 3f);
        move(Random.Range(3f, 6f));
        foodValue = 1;
    }
    private void defineNemo()
    {
        transform.localScale = RandomScale2D(1f, 2f);
        move(Random.Range(3f, 6f));
        foodValue = 2;
    }
    private void defineColourful()
    {
        transform.localScale = RandomScale2D(1f, 2f);
        move(Random.Range(3f, 6f));
        foodValue = 3;
    }
    private void defineGolden()
    {
        transform.localScale = RandomScale2D(0.8f, 1f);
        move(Random.Range(3f, 6f));
        foodValue = 10;
    }

    //Sizes, speed and damage values of damaging fish
    private void defineBadFish()
    {
        transform.localScale = RandomScale2D(1f, 2f);
        move(Random.Range(3f, 6f));
        damage = 1;
    }
    private void defineSword()
    {
        transform.localScale = RandomScale2D(2f, 4f);
        move(Random.Range(3f, 6f));
        damage = 3;
    }
    private void defineMine()
    {
        transform.localScale = RandomScale2D(7f, 8f);
        move(Random.Range(3f, 6f));
        damage = 10;
    }
    
    private Vector2 RandomScale2D(float min, float max)//Values for scale
    {
        float scale = Random.Range(min, max);
        Vector2 scaler2D = new Vector2(-scale, scale);
        return scaler2D;
    }
}