using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Atack : MonoBehaviour
{
   [SerializeField] private Transform controladorGolpe;
   [SerializeField] private float radioGolpe;
   [SerializeField] private float dañoGolpe;

    private Vector2 punch;
    private Rigidbody2D rb;
    private Animator animator;

      private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

      private void OnPunch(InputValue value)
    {
        punch = value.Get<Vector2>();
        Debug.Log("Puch");
    }

   private void Golpe(){
    Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorGolpe.position, radioGolpe);

    foreach (Collider2D colisionador in objetos){
        
        if(colisionador.CompareTag("Jugador2")){
            colisionador.transform.GetComponent<Jugador2>().TomarDaño(dañoGolpe);
        }    
     }
   }

   private void OnDrawGizmos() {
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(controladorGolpe.position, radioGolpe);
   }
}
