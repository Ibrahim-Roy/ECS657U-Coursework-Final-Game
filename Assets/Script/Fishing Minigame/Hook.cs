using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    private Rigidbody2D hook_Rigidbody;
    private Collider2D hook_Collider;

    public Transform startPos;

    [Range(1, 3)]
    public int difficulty; //If difficulties are added 
    float difficulty_speed;


    void Start()
    {
        //Fetch the Rigidbody from the GameObject with this script attached
        hook_Rigidbody = GetComponent<Rigidbody2D>();
        hook_Collider = GetComponent<BoxCollider2D>();

        if (difficulty == 1)
            difficulty_speed = 500f;
        else if (difficulty == 2)
            difficulty_speed = 300f;      
        else if (difficulty == 3)
            difficulty_speed = 200f;
    }

    // Update is called once per frame
    void Update()
    {
        resetHook();
        if (hook_Rigidbody.velocity.y == 0 && Input.GetMouseButtonDown(0))//Check if stationary and wait for mouse click
            descend();
        if (transform.position.y <= -4.0 && hook_Rigidbody.velocity.y < 4f)//Accelerate up, until max velocity
            ascend();
    }

    void ascend()//Accelerate upwards
    {
        hook_Rigidbody.AddForce(transform.up * (difficulty_speed / 12));//Slow then ascend slowly
    }
    void descend()//Accelerate downwards
    {
        hook_Rigidbody.AddForce(-transform.up * difficulty_speed);//Go down at speed based on difficulty
    }

    void resetHook()
    {
        if(hook_Rigidbody.velocity.y > 0 && transform.position.y > 4.0){
            hook_Rigidbody.velocity = new Vector2(0, 0);
            transform.position = startPos.position;
            hook_Collider.enabled = true;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        hook_Rigidbody.velocity = new Vector2(0, 4);
        if (col.gameObject.tag != "Player")
        {
            hook_Collider.enabled = false;
        }

    }
}