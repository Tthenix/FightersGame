using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [SerializeField] float moveSpeed; // Agregar esta línea para definir la velocidad
    private Rigidbody2D rb;
    private Vector2 moveDirection = Vector2.right; // Dirección predeterminada

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 5f);
    }

    public void SetMoveDirection(Vector2 direction)
    {
        moveDirection = direction;
    }

    private void Update()
    {
        Vector2 movement = moveDirection * moveSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);
    }
}
