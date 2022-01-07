using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HostileNPC : MonoBehaviour
{
    public float maximumRoamingDistance = 0;
    public float movementSpeed;
    public Slider healthBar;
    public int maxHealth;
    public ParticleSystem blood;
    public float detectionDistance;

    public virtual void takeDamage(int amount)
    {
        decrementHealth(amount);
        if(!blood.isPlaying)
        {
            blood.Play();
        }
    }

    protected GameObject target;
    protected float distanceToTarget;
    protected Vector2 randomRoamDestinationPosition;
    protected Vector2 originalPosition;
    protected Rigidbody2D rigidBody;
    protected Animator animator;
    protected int health;
    protected bool alive = true;
    protected bool chasingTarget = false;
    protected bool stuck = false;

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
                attackTarget();
            }
        }
    }

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

    protected void decrementHealth(int amount)
    {
        if(health > 0)
        {
            health -= amount;
            if(!healthBar.gameObject.activeSelf)
            {
                healthBar.gameObject.SetActive(true);
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
    }

    protected void animationHandler(Vector2 destination)
    {
        Vector2 walkingDirection = (destination - new Vector2(transform.position.x, transform.position.y)).normalized;
        animator.SetFloat("Horizontal", walkingDirection.x);
        animator.SetFloat("Vertical", walkingDirection.y);
    }

    protected virtual void attackTarget()
    {
        chasingTarget = true;
        Vector2 targetPosition = new Vector2(target.transform.position.x, target.transform.position.y);
        animationHandler(targetPosition);
        Vector2 nextStepPosition = Vector2.MoveTowards(transform.position, targetPosition, (movementSpeed)*Time.deltaTime);
        rigidBody.MovePosition(nextStepPosition);
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

    private IEnumerator waitForObstacleToBeCleared()
    {
        yield return new WaitForSeconds(1.5f);
        stuck = false;
    }
}
