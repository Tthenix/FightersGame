using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.InputSystem.EnhancedTouch;

public class Attack : MonoBehaviour
{
    [SerializeField] private Transform controladorGolpe;
    [SerializeField] private float radioGolpe;
    [SerializeField] private float dañoGolpe;
    [SerializeField] private float tiempoEntreAtaques;
    [SerializeField] private float tiempoSiguienteAtaque;
    [SerializeField] private AudioSource AtaqueSonido;

    int cg = 0;
    bool golpeo;

    private InputAction attackAction;
    private Rigidbody2D rb;
    private Animator animator;
    [SerializeField] private string comparador;

    private Player player; // Referencia al script Player para verificar la muerte

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        player = GetComponent<Player>(); // Obtener referencia al script Player

        attackAction = new InputAction("Attack");
        attackAction.AddBinding("<Keyboard>/h");
        attackAction.AddBinding("<Touchscreen>/primaryTouch/delta");

        attackAction.performed += ctx => Golpe();
        attackAction.Enable();
    }

    private void OnDestroy()
    {
        attackAction.Disable();
    }

    private void Update()
    {
        if (player.EstaMuerto()) return; // Verificar si el personaje está muerto y detener el código de ataque si es así

        if (tiempoSiguienteAtaque > 0)
        {
            tiempoSiguienteAtaque -= Time.deltaTime;
        }

        if (attackAction.triggered && !golpeo)
        {
            golpeo = true;
            cg += 1;
            switch (cg)
            {
                case 1:
                    animator.SetTrigger("Attack");
                    AtaqueSonido.Play();
                    break;
                case 2:
                    animator.SetTrigger("Attack1");
                    AtaqueSonido.Play();
                    break;
                case 3:
                    cg = 0;
                    animator.SetTrigger("Attack2");
                    AtaqueSonido.Play();
                    break;
            }
            StartCoroutine(RetardoGolpe(tiempoEntreAtaques));
        }
    }

    IEnumerator RetardoGolpe(float tiempoEntreAtaques)
    {
        yield return new WaitForSeconds(tiempoEntreAtaques);
        golpeo = false;
        yield return new WaitForSeconds(1f);
        cg = 0;
    }

    private void Golpe()
    {
        if (player.EstaMuerto()) return; // Verificar si el personaje está muerto y evitar el golpe si es así

        animator.SetTrigger("Attack");
        Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorGolpe.position, radioGolpe);

        foreach (Collider2D colisionador in objetos)
        {
            if (colisionador.CompareTag(comparador))
            {
                colisionador.transform.GetComponent<Player>().TomarDaño(dañoGolpe);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controladorGolpe.position, radioGolpe);
    }
}
