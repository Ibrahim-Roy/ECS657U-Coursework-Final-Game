using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAmmunition : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Hostile"))
        {
            other.gameObject.GetComponentInParent<HostileNPC>().takeDamage(1);
        }
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Destroy(this.gameObject);    
    }

}
