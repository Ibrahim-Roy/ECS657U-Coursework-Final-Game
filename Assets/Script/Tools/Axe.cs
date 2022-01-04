using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools : MonoBehaviour
{
    public string targetTag;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == targetTag)
        {
            other.gameObject.GetComponent<MineAbleObject>().takeDamage();
        }
    }
}
