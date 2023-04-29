using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private int speed = 5;

    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator animator;

    private void Awake(){
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void OnMovement(InputValue value){
        movement = value.Get<Vector2>();
        
            animator.SetFloat("X", movement.x);
            animator.SetFloat("Y", movement.y);

         
    }

    private void FixedUpdate(){  
     // rb.MovePosition(rb.position + movement * speed *Time.fixedDeltaTime);
        rb.velocity = movement * speed;
}
}
