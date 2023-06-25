// using UnityEngine;
// using UnityEngine.UI;

// public class GameManager : MonoBehaviour
// {
//     public int maxRounds = 3;
//     public Text roundText;
//     public Text winnerText;

//     private int roundCount = 0;
//     private bool isGameOver = false;

//     // Aquí puedes agregar referencias a los scripts de los personajes y otros elementos necesarios

//     private void Start()
//     {
//         StartRound();
//     }

//     private void StartRound()
//     {
//         // Reiniciar la vida de los personajes y realizar otras acciones necesarias para comenzar una nueva ronda

//         roundCount++;
//         roundText.text = "Round " + roundCount;
//     }

//     public void EndRound(string winner)
//     {
//         if (isGameOver)
//             return;

//         if (roundCount >= maxRounds)
//         {
//             isGameOver = true;
//             winnerText.text = "Game Over\n" + winner + " wins!";
//             // Realizar otras acciones necesarias para finalizar el juego
//         }
//         else
//         {
//             StartRound();
//         }
//     }
// }
