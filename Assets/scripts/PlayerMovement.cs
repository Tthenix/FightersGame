using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private const int speed = 4;
    private const float dashSpeed = 8; // La velocidad de dash es el doble de la velocidad normal
    private float dashDuration = 0.3f; // La duración del dash en segundos
    [SerializeField] private Text texto;

    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator animator;

    private bool isDashing = false;

    private float speedMultiplier = 1f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    #if UNITY_ANDROID || UNITY_IOS
        speedMultiplier = 1.5f;
    #endif


    }

    private void OnMovement(InputValue value)
    {
        movement = value.Get<Vector2>();
        animator.SetFloat("X", movement.x);
        animator.SetFloat("Y", movement.y);
        texto.text = ($"{movement.x}, {movement.y}");
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
            rb.velocity = movement * speed * speedMultiplier * Time.deltaTime;
        }
    }
}
