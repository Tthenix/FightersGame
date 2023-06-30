using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Image HP_P1;
    public GameObject gameObjectToDesactivate;

    [SerializeField] private float vidaMaxima;
    [SerializeField] private float vida;
    private Animator animator;
    private bool isDead = false;

    private float delay = 2f;
    private float currentTime = 0f;
    private bool isDeactivating = false;

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

        if (!isDeactivating)
        {
            isDeactivating = true;
            currentTime = 0f;
        }
    }

    void Update()
    {
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
    }
}
