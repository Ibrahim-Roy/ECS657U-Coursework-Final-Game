using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineAbleObject : MonoBehaviour
{

    public int maxHealth;
    public GameObject resourcePrefab;
    public float lifeCycleTime;
    public Animator animator;

    public void takeDamage()
    {
        if(health>0)
        {
            health--;
        }
        if ( health==0 && !destroyed)
        {
            destroyed = true;
            if(animator != null)
            {
                animator.SetBool("Mined", true);
            }
            GameObject item = Instantiate(resourcePrefab, transform.position, transform.rotation);
            Invoke("growBack", lifeCycleTime);
        }
    }

    private int health;
    private bool destroyed = false;

    private void Start()
    {
        health = maxHealth;
    }

    private void growBack()
    {
        destroyed = false;
        health = maxHealth;
        health = maxHealth;
        if(animator != null)
        {
            animator.SetBool("Mined", false);
        }
    }
}
