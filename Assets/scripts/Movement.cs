using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public void Move(Transform t, Vector3 p)
    {
        t.position = p;
    }

}
