using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KeyBoard_Movement : MonoBehaviour
{
    [SerializeField] KeyCode _up;
    [SerializeField] KeyCode _down;
    [SerializeField] KeyCode _left;
    [SerializeField] KeyCode _right;
    
    [SerializeField] float _step;

    Transform t;
    Vector3 p;
    Movement m;

    private void Awake()
    {
        t = GetComponent<Transform>();
        m = GetComponent<Movement>();
    }

    void Update()
    {
        try
        {
            if( Input.GetKeyDown( _up ) )
            {
                p  = new Vector3(t.position.x, t.position.y + _step);
                m.Move(t,p);
            }

            if( Input.GetKeyDown( _down ) )
            {
                p = new Vector3(t.position.x, t.position.y - _step);
                m.Move(t,p);
            }

            if( Input.GetKeyDown( _left ) )
            {
                p = new Vector3(t.position.x - _step, t.position.y);
                m.Move(t,p);
            }

            if( Input.GetKeyDown( _right ) )
            {
                p = new Vector3(t.position.x + _step, t.position.y);
                m.Move(t,p);
            }
        }
        catch (Exception) {
            Debug.LogError("Hay un error en el código");
        }
       
    }
    
}