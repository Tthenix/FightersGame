using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.InputSystem.EnhancedTouch;

public class Attack2 : MonoBehaviour
{
    [SerializeField] private Transform controladorGolpe2;
    [SerializeField] private float radioGolpe;
    [SerializeField] private float dañoGolpe;
    [SerializeField] private float tiempoEntreAtaques;
    [SerializeField] private float tiempoSiguienteAtaque;

    int cg = 0;
    bool golpeo;

    private InputAction attackAction;
    private Rigidbody2D rb;
    private Animator animator;
    [SerializeField] private string comparador;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        attackAction = new InputAction("Attack");
        attackAction.AddBinding("<Gamepad>/buttonNorth");

        attackAction.performed += ctx => Golpe();
        attackAction.Enable();
    }

    private void OnDestroy()
    {
        attackAction.Disable();
    }

    private void Update()
    {
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
                    break;
                case 2:
                    animator.SetTrigger("Attack1");
                    break;
                case 3:
                    cg = 0;
                    animator.SetTrigger("Attack2");
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
        animator.SetTrigger("Attack");
        Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorGolpe2.position, radioGolpe);

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
        Gizmos.DrawWireSphere(controladorGolpe2.position, radioGolpe);
    }
}