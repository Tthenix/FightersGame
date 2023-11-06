using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTest : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] GameObject bullet;
    Vector2 moveValue;

    public virtual void Move(Vector2 value)
    {
        moveValue = value * speed * Time.deltaTime;
    }

    public void Shoot()
    {
        GameObject.Instantiate(bullet, transform.position, transform.rotation);
    }

    private void Update()
    {
        transform.Translate(moveValue);
    }
}
