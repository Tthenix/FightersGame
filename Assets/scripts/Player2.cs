using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player2 : MonoBehaviour
{
    public Image HP_P2;

    [SerializeField] private float vidaMaxima;
    [SerializeField] private float vida;
    private Animator animator;
    bool isDead = false;

    private Attack2 attack; // Referencia al script Attack2 para desactivar el ataque

    private void Start()
    {
        animator = GetComponent<Animator>();
        attack = GetComponent<Attack2>(); // Obtener referencia al script Attack2
    }

    public void TomarDaño(float daño)
    {
        if (isDead) return; // Si ya está muerto, no se toma más daño

        vida -= daño;

        if (vida <= 0)
        {
            Muerte();
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

        attack.enabled = false; // Desactivar el script Attack2 cuando el jugador está muerto
    }

    void Update()
    {
        HP_P2.fillAmount = vida / vidaMaxima;
    }
}
