using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Opciones : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Data data;
    [SerializeField] private Slider slider;

    public void BotonOpciones()
    {
        SceneManager.LoadScene(0);
    }
    public void CambiarVolumne(float volumen){

        audioSource.volume = volumen;
        data.Volumen = volumen;
    }

    private void Start()
    {
        slider.value = data.Volumen;
    }
}
