using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.InputSystem.EnhancedTouch;

public class Player2 : MonoBehaviour
{
    //Player
    public Image HP_P2;
    public GameObject gameObjectToDesactivate;

    [SerializeField] private float vidaMaxima;
    [SerializeField] private float vida;
    
    private Animator animator;
    private bool isDead = false;

    private float delay = 2f;
    private float currentTime = 0f;
    private bool isDeactivating = false;

    //Data
    [SerializeField] private Data data;

    //Attack
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
    
    [SerializeField] private string comparador;

    private Player player; // Referencia al script Player para verificar la muerte

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TomarDaño(float daño)
    {
        if (isDead) return; // Si ya está muerto, no se toma más daño

        vida -= daño;

        if (vida <= 0)
        {
            Invoke("Muerte" ,1.0f);
            data.PuntajeP1 += 1;
        }
    }

    public bool EstaMuerto() // Método público para verificar si el personaje está muerto
    {
        return isDead;
    }

    private void Muerte()
    {
        animator.SetTrigger("Death");
        isDead = true;
        transform.GetComponent <PlayerMovement>().IsDead = true; 

        if (!isDeactivating)
        {
            isDeactivating = true;
            currentTime = 0f;
        }
    }

    void Update()
    {
        HP_P2.fillAmount = vida / vidaMaxima;

        if (isDeactivating)
        {
            currentTime += Time.deltaTime;

            if (currentTime >= delay)
            {
                gameObjectToDesactivate.SetActive(false);
                isDeactivating = false;
            }
        }

        //Attack
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

    //Attack
    private void Awake()
    {
        
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

            // Buscar el GameObject con el script Player2 y obtener la referencia
        player = GameObject.FindObjectOfType<Player>();

        attackAction = new InputAction("Attack");
        attackAction.AddBinding("<Gamepad>/buttonNorth");

        attackAction.performed += ctx => Golpe();
        attackAction.Enable();
    }

    private void OnDestroy()
    {
        attackAction.Disable();
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
