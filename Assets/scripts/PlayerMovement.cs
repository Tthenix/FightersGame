using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;  // Referencia al Rigidbody2D del jugador
    public Transform groundCheck;  // Punto de chequeo de si el jugador está en el suelo
    public LayerMask groundLayer;  // Capa del suelo
    private Animator animator; //import animator

    private bool canDash = true;  // Booleano que indica si el jugador puede hacer dash
    private bool isDashing;  // Booleano que indica si el jugador está haciendo dash
    private float dashingPower = 8f;  // Fuerza del dash
    private float dashingTime = 0.2f;  // Duración del dash
    private float dashingCooldown = 1f;  // Tiempo de espera antes de poder hacer otro dash

    private float horizontal;  // Entrada horizontal del jugador
    private float speed = 6f;  // Velocidad horizontal del jugador
    private float jumpingPower = 12f;  // Fuerza del salto del jugador
    private bool isFacingRight = true;  // Booleano que indica si el jugador está mirando a la derecha

    [SerializeField] private TrailRenderer tr;  // Referencia al TrailRenderer del jugador

    void Start(){
        animator = GetComponent<Animator>();
    }
    // Esta función se llama una vez por frame
    void Update()
    {
        if (isDashing)  // Si el jugador está haciendo dash, no hace nada
        {
            return;
        }

        if (!isFacingRight && horizontal > 0f)  // Si el jugador está mirando hacia la izquierda y se mueve a la derecha, voltear al jugador
        {
            Flip();
        }
        else if (isFacingRight && horizontal < 0f)  // Si el jugador está mirando hacia la derecha y se mueve a la izquierda, voltear al jugador
        {
            Flip();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)  // Si el jugador presiona Shift izquierdo y puede hacer dash, inicia el dash
        {
            StartCoroutine(Dash());  // Coroutine que permite hacer dash
        }
    }

    // Esta función se llama en intervalos regulares de tiempo
    private void FixedUpdate()
    {
        if (isDashing)  // Si el jugador está haciendo dash, no se actualiza su velocidad
        {
            return;
        }

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);  // Establece la velocidad horizontal del jugador
        if (IsGrounded()) // Verifica si el jugador está en el suelo
        {
            animator.SetBool("IsJump", false); // Desactiva la animación de salto si el jugador está en el suelo
        }
    }

    // Esta función se llama cuando el jugador salta
    // Esta función se llama cuando el jugador salta
        public void Jump(InputAction.CallbackContext context)
        {
            if (context.performed && IsGrounded())  // Si el jugador presiona el botón de salto y está en el suelo, hacer que salte
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                animator.SetBool("IsJump", true); // Activa la animación de salto
            }
            else if (context.canceled && rb.velocity.y > 0f)  // Si el jugador deja de presionar el botón de salto mientras está en el aire, reducir su velocidad vertical
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }
        }



    // Esta función devuelve verdadero si el jugador está en el suelo
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.7f, groundLayer);// Devuelve true si el jugador está en el suelo
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    public void Move(InputAction.CallbackContext context)
    {
        if(horizontal  != 0f){
            animator.SetBool("IsRunning", true);
        }
        else{
            animator.SetBool("IsRunning", false);
        }

        horizontal = context.ReadValue<Vector2>().x; // Lee la entrada horizontal del jugador
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
