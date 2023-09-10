using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Player2 : MonoBehaviour
{
    // Referencias a objetos y variables del jugador
    public Image HP_P2;
    public GameObject gameObjectToDesactivate;
    [SerializeField] private float vidaMaxima;
    [SerializeField] private float vida;
    private Animator animator;
    private bool isDead = false;
    private float delay = 1f;
    private float currentTime = 0f;
    private bool isDeactivating = false;
    public GameObject panel;
    private bool isGameFrozen = false; // Variable para controlar la congelación del juego

    // Datos del jugador
    [SerializeField] private Data data;

    // Ataque
    [SerializeField] private Transform controladorGolpe;
    [SerializeField] private float radioGolpe;
    [SerializeField] private float dañoGolpe;
    [SerializeField] private float tiempoEntreAtaques;
    [SerializeField] private float tiempoSiguienteAtaque;
    [SerializeField] private AudioSource AtaqueSonido;
    int comboCounter = 0;
    bool golpeo;
    private InputAction attackAction;
    private Rigidbody2D rb;
    [SerializeField] private string comparador;
    private Player player; // Referencia al script Player para verificar la muerte

    private void Start()
    {
        // Inicialización al inicio del juego
        animator = GetComponent<Animator>();
        panel.SetActive(false);
    }

    // Función para manejar el daño al jugador
    public void TomarDaño(float daño)
    {
        if (isDead) return; // Si ya está muerto, no se toma más daño

        vida -= daño;
        Update();

        if (vida <= 0)
        {
            Invoke("Muerte", 0f);
            data.PuntajeP1 += 1;
        }
    }

    public void Destroy()
    {
        Destroy(this);
    }

    // Verificar si el jugador está muerto
    public bool EstaMuerto()
    {
        return isDead;
    }

    // Función para manejar la muerte del jugador
    private void Muerte()
    {
        animator.SetTrigger("Death"); // Ejecuta la animación "Death"
        isDead = true;
        attackAction.Disable();
        transform.GetComponent<PlayerMovement>().IsDead = true;

        StartCoroutine(CongelarJuegoDespuesDeDelay(1f)); // Congela el juego después de 1 segundo
    }

    private IEnumerator CongelarJuegoDespuesDeDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay); // Espera durante el retraso en tiempo real
        Time.timeScale = 0f; // Congela el juego
        isGameFrozen = true; // Marca el juego como congelado
        StartCoroutine(ActivarPanelGameOver()); // Inicia la corutina para activar el panel de Game Over
    }

    // Corutina para activar el panel de Game Over después de 3 segundos
    private IEnumerator ActivarPanelGameOver()
    {
        yield return new WaitForSecondsRealtime(1.5f); // Espera durante 3 segundos en tiempo real
        panel.SetActive(true); // Activa el panel de Game Over
    }

    private void Update()
    {
        if (isDead == false)
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

            // Ataque
            if (player.EstaMuerto()) return; // Verificar si el personaje está muerto y detener el código de ataque si es así

            if (tiempoSiguienteAtaque > 0)
            {
                tiempoSiguienteAtaque -= Time.deltaTime;
            }

            if (attackAction.triggered && !golpeo)
            {
                golpeo = true;
                comboCounter++;

                // Reiniciar el combo si se alcanza el límite
                if (comboCounter > 2)
                {
                    comboCounter = 0;
                }

                // Activar la animación de ataque según el combo actual
                switch (comboCounter)
                {
                    case 1:
                        animator.SetTrigger("Attack");
                        break;
                    case 2:
                        comboCounter = 0;
                        animator.SetTrigger("Attack1");
                        break;
                }

                AtaqueSonido.Play();
                tiempoSiguienteAtaque = tiempoEntreAtaques; // Reiniciar el tiempo de siguiente ataque
                StartCoroutine(RetardoGolpe(tiempoEntreAtaques));
            }
        }
    }

    // Función para manejar el ataque del jugador
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

    // Dibujar un gizmo en el editor para mostrar el alcance del ataque
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controladorGolpe.position, radioGolpe);
    }

    // Inicialización del jugador y el ataque
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Buscar el GameObject con el script Player y obtener la referencia
        player = GameObject.FindObjectOfType<Player>();

        attackAction = new InputAction("Attack");
        attackAction.AddBinding("<Gamepad>/buttonNorth");

        attackAction.performed += ctx => Golpe();
        attackAction.Enable();
    }

    // Liberar recursos cuando el objeto se destruye
    private void OnDestroy()
    {
        attackAction.Disable();
    }

    // Retardo entre ataques
    IEnumerator RetardoGolpe(float tiempoEntreAtaques)
    {
        yield return new WaitForSeconds(tiempoEntreAtaques);
        golpeo = false;
        yield return new WaitForSeconds(1f);
        comboCounter = 0;
    }
}