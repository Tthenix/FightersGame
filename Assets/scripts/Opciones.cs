using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Opciones : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    public void BotonOpciones()
    {
        SceneManager.LoadScene(0);
    }
    public void CambiarVolumne(float volumen){

        audioMixer.SetFloat("Volumen", volumen);
    }
}
