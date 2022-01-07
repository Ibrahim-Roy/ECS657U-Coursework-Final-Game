using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class HostileNPC : MonoBehaviour
{
    public Slider healthBar;
    public ParticleSystem blood;
    public float maximumRoamingDistance = 0;
    public float detectionDistance;
    public float movementSpeed;
    public float stoppingDistance;
    public int maxHealth;

    public virtual void takeDamage(int amount)
    {
        decrementHealth(amount);
    }

    protected GameObject target;
    protected Rigidbody2D rigidBody;
    protected Animator animator;
    protected float distanceToTarget;
    protected Vector2 originalPosition;
    protected Vector2 randomRoamDestinationPosition;
    protected int health;
    protected bool alive = true;
    protected bool chasingTarget = false;
    protected bool stuck = false;
    protected bool backUp = false;
    protected bool attacking = false;

    protected virtual void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    protected virtual void Start()
    {
        originalPosition = new Vector2(transform.position.x, transform.position.y);
        setRandomRoamDestination();
        animationHandler(randomRoamDestinationPosition); 
        health = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.fillRect.GetComponentInChildren<Image>().color = Color.red;     
    }

    protected virtual void FixedUpdate()
    {
        if(alive)
        {
            distanceToTarget = (target.transform.position - transform.position).magnitude;
            if(distanceToTarget > detectionDistance || stuck)
            {
                roamWorldRandomly();
            }
            else if(distanceToTarget <= detectionDistance)
            {
                if(!attacking)
                {
                    chaseTarget();
                }
            }
        }
    }

    protected virtual void chaseTarget()
    {
        chasingTarget = true;
        if(distanceToTarget>stoppingDistance)
        {
            Vector2 targetPosition = new Vector2(target.transform.position.x, target.transform.position.y);
            animationHandler(targetPosition);
            Vector2 nextStepPosition = Vector2.MoveTowards(transform.position, targetPosition, (movementSpeed)*Time.deltaTime);
            rigidBody.MovePosition(nextStepPosition);
        }
        else if(distanceToTarget<stoppingDistance - 1f)
        {
            Vector2 targetPosition = new Vector2(target.transform.position.x, target.transform.position.y);
            Vector2 nextStepPosition = Vector2.MoveTowards(transform.position, targetPosition, (-movementSpeed)*Time.deltaTime);
            rigidBody.MovePosition(nextStepPosition);
        }
        else
        {
            if(!backUp)
            {
                attackTarget();
            }
        }
    }

    protected virtual void OnCollisionStay2D(Collision2D other) {
        if(!other.gameObject.CompareTag(target.tag))
        {
            if(chasingTarget)
            {
                stuck = true;
                StartCoroutine(waitForObstacleToBeCleared());
            }
            setRandomRoamDestination();
            roamWorldRandomly();
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag(target.tag))
        {
            if(alive)
            {
                attackTarget();
            }
        }
    }

    protected abstract void attackTarget();

    protected void setRandomRoamDestination()
    {
        randomRoamDestinationPosition = new Vector2(
            Random.Range(originalPosition.x - maximumRoamingDistance, originalPosition.x + maximumRoamingDistance),
            Random.Range(originalPosition.y - maximumRoamingDistance, originalPosition.y + maximumRoamingDistance)
        );
    }

    protected void roamWorldRandomly()
    {
        Vector2 nextStepPosition = Vector2.MoveTowards(transform.position, randomRoamDestinationPosition, (movementSpeed)*Time.deltaTime);
        rigidBody.MovePosition(nextStepPosition);
        if(Vector2.Distance(transform.position, randomRoamDestinationPosition) < 0.5)
        {
            setRandomRoamDestination();
            animationHandler(randomRoamDestinationPosition); 
        }
    }

    protected void animationHandler(Vector2 destination)
    {
        Vector2 walkingDirection = (destination - new Vector2(transform.position.x, transform.position.y)).normalized;
        animator.SetFloat("Horizontal", walkingDirection.x);
        animator.SetFloat("Vertical", walkingDirection.y);
    }

     protected void decrementHealth(int amount)
    {
        if(health > 0)
        {
            health -= amount;
            if(!blood.isPlaying)
            {
                blood.Play();
            }
            if(!healthBar.gameObject.activeSelf)
            {
                healthBar.gameObject.SetActive(true);
            }
        }
        if(health <= 0)
        {
            health = 0;
            healthBar.gameObject.SetActive(false);
            animator.SetTrigger("Kill");
            alive = false;
            rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        healthBar.value = health;
    }

    protected IEnumerator waitForObstacleToBeCleared()
    {
        yield return new WaitForSeconds(1.5f);
        stuck = false;
    }
}
