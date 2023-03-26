using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limites : MonoBehaviour
{
    public float speed = 4f;
    float minX, maxX, minY, maxY; // definir los límites del movimiento

    void Start()
    {
        // calcular los límites del movimiento
        float camDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
        minY = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, camDistance)).y + 1;
        maxY = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, camDistance)).y - 1;
        minX = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, camDistance)).x + 2;
        maxX = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, camDistance)).x - 2;
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float v = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        // limitar la posición del personaje dentro del área rectangular
        float newX = Mathf.Clamp(transform.position.x + h, minX, maxX);
        float newY = Mathf.Clamp(transform.position.y + v, minY, maxY);
        transform.position = new Vector3(newX, newY, transform.position.z);
    }
}
