using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private Animator animator;
    private float movementSpeed = 2.0f;
    private float horizontalInput;
    private float verticalInput;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();   
    }

    private void Update()
    {
        inputHandler();
        animationHandler();
    }

    private void FixedUpdate()
    {
        movementHandler();
    }

    private void animationHandler()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0;
        Vector3 mouseDirection = (mouseWorldPosition - transform.position).normalized;
        animator.SetFloat("Horizontal", mouseDirection.x);
        animator.SetFloat("Vertical", mouseDirection.y);
        animator.SetFloat("Speed", rigidBody.velocity.magnitude);
    }

    private void inputHandler()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    private void movementHandler()
    {
        rigidBody.velocity = new Vector2(horizontalInput, verticalInput) * movementSpeed;
    }
}
