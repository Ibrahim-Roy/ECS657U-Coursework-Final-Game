using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Tree")
        {
            other.gameObject.GetComponent<MineAbleObject>().takeDamage();
        }
    }
}
