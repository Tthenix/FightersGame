using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP_BAR_P2 : MonoBehaviour
{
    public Image HP_P1;

    public float vidaActual;
    public float vidaMaxima;

    void Update()
    {
        HP_P1.fillAmount = vidaActual / vidaMaxima;
    }
}
