using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public bool Left { get; set; }
    public bool Player1 { get; set; }
    private int counterPlayer = 0;

    public int GetCounterPlayer()
    {
        return counterPlayer;
    }

    public void AddCounterPlayer()
    {
        counterPlayer++;
    }

}
