using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // Referencias a objetos y variables del jugador
    public Image HP_P1;
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
    [SerializeField] private ParticleSystem damageParticles; // Arrastra el sistema de partículas aquí desde el Inspector


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
    private Player2 player2;

    // Ataque de energia
    private InputAction shootAction;
    [SerializeField] private Transform energyBulletController;
    [SerializeField] private GameObject bullet;
    bool isShooting;

    // sistema de energia
    public Image Energy_P1;
    [SerializeField] private float energiaMaxima;
    [SerializeField] private float EnergiaActual;

    private void Start()
    {
        // Inicialización al inicio del juego
        animator = GetComponent<Animator>();
        panel.SetActive(false);
    }

    // Función para manejar el daño al jugador
    public void TomarDaño(float daño)
    {
        if (isDead) return;

        vida -= daño;
        GetComponent<Animator>().Play("Player_TakeDamage");
        Update();

        if (vida <= 0)
        {
            Invoke("Muerte", 0f);
            data.PuntajeP2 += 1;
        }
        else
        {
            // Activa el sistema de partículas de daño
            damageParticles.Play();

            // Configura un temporizador para desactivar las partículas después de un tiempo
            StartCoroutine(DesactivarParticulasDeDaño());

            // Aumenta la energía
            EnergiaActual += 5;
        }
    }
    private IEnumerator DesactivarParticulasDeDaño()
    {
        yield return new WaitForSeconds(1f); // Cambia este valor según lo que desees
        damageParticles.Stop(); // Detiene las partículas después de 2 segundos (ajusta el tiempo según tus necesidades)
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
        shootAction.Disable();
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
        if (isDead) return;

        HP_P1.fillAmount = vida / vidaMaxima;

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
        if (player2.EstaMuerto()) return;

        if (tiempoSiguienteAtaque > 0)
        {
            tiempoSiguienteAtaque -= Time.deltaTime;
        }

        // Controlar el ataque y el combo
        if (attackAction.triggered && tiempoSiguienteAtaque <= 0 && !golpeo)
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
        // sistema de energia recarga
        EnergiaActual += Time.deltaTime * 5;

        if (EnergiaActual > energiaMaxima)
        {
            EnergiaActual = energiaMaxima;
        }

        Energy_P1.fillAmount = EnergiaActual / energiaMaxima;
    }
    // disparo de energia
    private void Shoot()
    {
        if (EnergiaActual >= 20 && !isShooting)
        {
            EnergiaActual -= 20;
            isShooting = true;
            GetComponent<Animator>().Play("Player_EnergyShoot");

        

            // Inicia el tiempo de recarga
            StartCoroutine(DelayShot());
        }
    }

    private IEnumerator DelayShot()
    {
        yield return new WaitForSeconds(0.5f); // Espera un segundo
        // Crea la bala
        Instantiate(bullet, energyBulletController.position, energyBulletController.rotation);
        // Finaliza el tiempo de recarga
        isShooting = false;
    }
    
    // Función para manejar el ataque del jugador
    private void Golpe()
    {
        if (player2.EstaMuerto()) return;

        animator.SetTrigger("Attack");
        Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorGolpe.position, radioGolpe);

        foreach (Collider2D colisionador in objetos)
        {
            if (colisionador.CompareTag(comparador))
            {
                colisionador.transform.GetComponent<Player2>().TomarDaño(dañoGolpe);
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

        // Buscar el GameObject con el script Player2 y obtener la referencia
        player2 = GameObject.FindObjectOfType<Player2>();

        attackAction = new InputAction("Attack");
        attackAction.AddBinding("<Keyboard>/h");
        attackAction.AddBinding("<Touchscreen>/primaryTouch/delta");

        attackAction.performed += ctx => Golpe();
        attackAction.Enable();

         // Agregar la binding para el disparo de energía
        shootAction = new InputAction("Shoot");
        shootAction.AddBinding("<Keyboard>/g");

        shootAction.performed += ctx => Shoot();
        shootAction.Enable();
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
