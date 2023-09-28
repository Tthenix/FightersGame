using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownController : MonoBehaviour
{
    public int countdownTime;
    public Text countDownDisplay;
    public GameObject[] objectsToDeactivate; // Arreglo de GameObjects para desactivar/activar

    private void Start()
    {
        StartCoroutine(CountdownToStart());
    }

    IEnumerator CountdownToStart()
    {
        // Desactivar los GameObjects al comienzo
        foreach (GameObject obj in objectsToDeactivate)
        {
            obj.SetActive(false);
        }

        while (countdownTime > 0)
        {
            countDownDisplay.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);

            countdownTime--;
        }

        countDownDisplay.text = "GO!";

        // Activar los GameObjects cuando aparezca "GO!"
        foreach (GameObject obj in objectsToDeactivate)
        {
            obj.SetActive(true);
        }

        yield return new WaitForSeconds(1f);
        countDownDisplay.gameObject.SetActive(false);
    }
}