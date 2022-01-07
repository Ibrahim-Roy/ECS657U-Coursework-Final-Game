using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monsters : HostileNPC
{
    protected override void attackTarget()
    {
        if(!attacking)
        {
            attacking = true;
            animator.SetTrigger("Attack");
            StartCoroutine(damage());
        }
        if(!backUp)
        {
            StartCoroutine(dodgeBack(stoppingDistance));
        }
    }

    private IEnumerator damage()
    {
        yield return new WaitForSeconds(0.25f);
        target.GetComponent<Player>().takeDamage(1);
        yield return new WaitForSeconds(0.25f);
        attacking = false;
    }

    private IEnumerator dodgeBack(float distance)
    {
        backUp = true;
        stoppingDistance = (distance*2) + 0.3f;
        yield return new WaitForSeconds(2f);
        stoppingDistance = distance;
        backUp = false;
    }
}
