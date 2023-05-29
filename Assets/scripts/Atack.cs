using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Atack : MonoBehaviour
{
   [SerializeField] private Transform controladorGolpe;
   [SerializeField] private float radioGolpe;
   [SerializeField] private float dañoGolpe;
   [SerializeField] private float tiempoEntreAtaques;
   [SerializeField] private float tiempoSiguienteAtaque;

    int cg = 0;
    bool golpeo;

    private Vector2 punch;
    private Rigidbody2D rb;
    private Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();    
    }

    private void Update()
    {
        if (tiempoSiguienteAtaque > 0)
        {
            tiempoSiguienteAtaque -= Time.deltaTime;
        }

        // if (Input.GetButtonDown("Fire1")){
        //     Golpe();
        //     tiempoSiguienteAtaque = tiempoEntreAtaques;
        // }

        if (Input.GetKeyDown(KeyCode.H) && !golpeo)
        {
            Golpe();
            golpeo = true;
            cg += 1;
            switch(cg)
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
            StartCoroutine(Retardogolpe(tiempoEntreAtaques));
        }
    }

  
    

    IEnumerator Retardogolpe(float tiempoEntreAtaques)
    {
        
        yield return new WaitForSeconds(tiempoEntreAtaques);
        golpeo = false;
        yield return new WaitForSeconds(1f);
        cg = 0;
    }

   private void Golpe()
   {
    animator.SetTrigger("Attack");
    Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorGolpe.position, radioGolpe);

    foreach (Collider2D colisionador in objetos){
        
        if(colisionador.CompareTag("Enemigo")){
            colisionador.transform.GetComponent<Enemigo>().TomarDaño(dañoGolpe);
        }    
     }
   }

   private void OnDrawGizmos() {
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(controladorGolpe.position, radioGolpe);
   }
}
