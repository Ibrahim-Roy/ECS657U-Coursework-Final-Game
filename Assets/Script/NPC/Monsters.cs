using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monsters : HostileNPC
{
    protected override void attackTarget()
    {
        if(!attacking)
        {
            StartCoroutine(attack());
        }
    }

    private IEnumerator attack()
    {
        
        attacking = true;
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.25f);
        target.GetComponent<Player>().takeDamage(1);
        yield return new WaitForSeconds(0.25f);
        attacking = false;
        if(!backUp)
        {
            StartCoroutine(dodgeBack(stoppingDistance));
        }
    }

    private IEnumerator dodgeBack(float distance)
    {
        backUp = true;
        stoppingDistance = (distance*2) + 0.3f;
        yield return new WaitForSeconds(1f);
        stoppingDistance = distance;
        backUp = false;
    }
}
