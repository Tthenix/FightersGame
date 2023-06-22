using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemigo : MonoBehaviour
{
   public Image HP_P1;
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
        HP_P1.fillAmount = vida / vidaMaxima;
    }
}