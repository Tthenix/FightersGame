using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private int speed = 8;
    private float dashSpeed = 16; // La velocidad de dash es el doble de la velocidad normal
    private float dashDuration = 0.3f; // La duración del dash en segundos

    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator animator;

    private bool isDashing = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void OnMovement(InputValue value)
    {
        movement = value.Get<Vector2>();
        animator.SetFloat("X", movement.x);
        animator.SetFloat("Y", movement.y);
    }

    private void OnDash(InputValue value)
    {
        if (!isDashing && value.isPressed)
        {
            Vector2 dashDirection = movement.normalized;
            rb.velocity = dashDirection * dashSpeed;
            isDashing = true;
            StartCoroutine(StopDashingAfterDelay());
        }
    }

    private IEnumerator StopDashingAfterDelay()
    {
        yield return new WaitForSeconds(dashDuration);
        rb.velocity = Vector2.zero;
        isDashing = false;
    }

    private void FixedUpdate()
    {
        if (!isDashing)
        {
            rb.velocity = movement * speed;
        }
    }
}
