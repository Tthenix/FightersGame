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

 private void Start() 
 {
    animator = GetComponent <Animator>();
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

 private void Muerte()
 {
    animator.SetTrigger("Death");
    isDead = true;
 }

     void Update()
    {
        HP_P2.fillAmount = vida / vidaMaxima;
    }
}