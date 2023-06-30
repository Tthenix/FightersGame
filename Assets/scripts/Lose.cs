using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lose : MonoBehaviour
{
    public GameObject gameObjectToDesactivate;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            gameObjectToDesactivate.SetActive(false);
        }

        if(Input.GetKeyDown(KeyCode.I))
        {
            gameObjectToDesactivate.SetActive(true);
        }
    }
}
