using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    Rigidbody2D hook_Rigidbody;
    int difficulty = 0; //If difficulties are added 
    float difficulty_speed;
    public Transform startPos;

    void Start()
    {
        //Fetch the Rigidbody from the GameObject with this script attached
        hook_Rigidbody = GetComponent<Rigidbody2D>();
        if (difficulty == 0)
            difficulty_speed = 200f;
        else if (difficulty == 1)
            difficulty_speed = 300f;      
        else if (difficulty == 2)
            difficulty_speed = 500f;
    }

    // Update is called once per frame
    void Update()
    {
        resetHook();
        if (hook_Rigidbody.velocity.y == 0 && Input.GetMouseButtonDown(0))//Check if stationary and wait for mouse click
            descend();
        if (transform.position.y <= -4.0)//Deccelerate and ascend 
            ascend();
    }

    void ascend()
    {
        hook_Rigidbody.AddForce(transform.up * 2f);//Slow then ascend slowly
    }
    void descend()
    {
        hook_Rigidbody.AddForce(-transform.up * difficulty_speed);//Go down at speed based on difficulty
    }

    void resetHook()
    {
        if(hook_Rigidbody.velocity.y > 0 && transform.position.y > 4.0){
            hook_Rigidbody.velocity = new Vector2(0, 0);
            transform.position = startPos.position;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Trigger2");
        hook_Rigidbody.velocity = new Vector2(0, 4);
    }
}