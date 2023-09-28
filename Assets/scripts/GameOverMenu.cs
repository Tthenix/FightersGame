using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    // GameOverPanel
    public GameObject panel1;
    public GameObject panel2;

    public Data data;

    public Text P1score;
    public Text P2score;

    private void Start()
    {
        // Convierte el valor int a string antes de asignarlo
        P1score.text = data.PuntajeP1.ToString();
        P2score.text = data.PuntajeP2.ToString();
    }

    public void JugarDeNuevo()
    {
        SceneManager.LoadScene(1);

        // Establecer la escala de tiempo a 1 para descongelar el juego
        Time.timeScale = 1f;

        panel1.SetActive(false);
        panel2.SetActive(false);
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
}