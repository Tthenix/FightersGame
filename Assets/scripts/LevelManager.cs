using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void BotonStart()
    {
        SceneManager.LoadScene(1);
    }

     public void BotonOpciones()
    {
        SceneManager.LoadScene(2);
    }
         public void BotonMenu()
    {
        SceneManager.LoadScene(0);
    }
}
