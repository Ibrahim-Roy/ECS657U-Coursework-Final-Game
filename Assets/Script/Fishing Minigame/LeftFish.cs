using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftFish : MonoBehaviour
{
    Rigidbody2D leftFish_Rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        leftFish_Rigidbody = GetComponent<Rigidbody2D>();
        move(5);
    }

    void move(int u)
    {
        leftFish_Rigidbody.velocity = new Vector2(u, 0);
    }
    
    void stopMove()
    {
        leftFish_Rigidbody.velocity = new Vector2(0, 0);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "hook")
        {
            Debug.Log("Trigger");
            stopMove();
            this.transform.parent = col.transform;      
            leftFish_Rigidbody.velocity = new Vector2(0, 4);
        }

        if (col.gameObject.tag == "Player")
        {
            Destroy(gameObject);//Add sound here
        }
    }
}
