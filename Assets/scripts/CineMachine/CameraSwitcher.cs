using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSwitcher : MonoBehaviour
{
    public Transform player1;
    public Transform player2;

    public float switchDistance = 10f;

    [SerializeField]
    private CinemachineVirtualCamera distantCamera;

    [SerializeField]
    private CinemachineVirtualCamera closeCamera;

    private void Update()
    {
        float distance = Vector3.Distance(player1.position, player2.position);

        if (distance <= switchDistance)
        {
            closeCamera.Priority = 1;
            distantCamera.Priority = 0;
        }
        else
        {
            closeCamera.Priority = 0;
            distantCamera.Priority = 1;
        }
    }
}
