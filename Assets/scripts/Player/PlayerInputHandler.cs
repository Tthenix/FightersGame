using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    Player player;

    [SerializeField] List<GameObject> prefabs = new List<GameObject>();
    [SerializeField] Transform playerSpawnLeft;
    [SerializeField] Transform playerSpawnRight;
    Vector3 spawnPosition;
    int orderPlayer = 0;
    private void Start()
    {
        SpawnerController Sc = FindObjectOfType<SpawnerController>();

        if (Sc.GetCounterPlayer() == 0)
        {
            Sc.AddCounterPlayer();
            int position = Random.Range(0, 2);
            spawnPosition = position == 0 ? playerSpawnLeft.position : playerSpawnRight.position;
            if (position == 0)
            {
                Sc.Left = true;
            }
            orderPlayer = Random.Range(0, prefabs.Count);

            if (orderPlayer == 0)
            {
                Sc.Player1 = true;
            }

        }
        else
        {
            spawnPosition = Sc.Left == false ? playerSpawnLeft.position : playerSpawnRight.position;

            if (Sc.Player1 == true)
            {
                orderPlayer = Sc.GetCounterPlayer();
            }
            Sc.AddCounterPlayer();
        }
        player = GameObject.Instantiate(prefabs[orderPlayer], spawnPosition, Quaternion.identity).GetComponent<Player>();
        // Instancia al jugador en una posición aleatoria (izquierda o derecha).
    }

    // public void Move(InputAction.CallbackContext context)
    // {
    //     if (player)
    //         player.Move(context.ReadValue<Vector2>());
    // }

    // public void Shoot(InputAction.CallbackContext context)
    // {
    //     if (player && context.started)
    //     {
    //         player.Shoot();
    //     }
    // }
}