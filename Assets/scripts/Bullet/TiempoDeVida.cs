using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiempoDeVida : MonoBehaviour
{
  [SerializeField] private float tiempoDeVida; // Agrega esta línea
    void Start()
    {
        Destroy(gameObject, tiempoDeVida);
    }
}
