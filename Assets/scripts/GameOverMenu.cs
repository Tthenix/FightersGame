using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    // GameOverPanel
    public GameObject panel1;
    public GameObject panel2;

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