using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Hostile"))
        {
            other.gameObject.GetComponentInParent<HostileNPC>().takeDamage(1);
        }
    }
}
