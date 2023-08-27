using UnityEngine;

public class RandomObjectSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] objectsToSpawn;
    [SerializeField] Transform[] spawnLocations;

    [SerializeField] float spawnTime;
    [SerializeField] float spawnRate;

    // Agregar dos puntos para determinar las áreas izquierda y derecha
    [SerializeField] Transform leftBoundary;
    [SerializeField] Transform rightBoundary;

    void Start()
    {
        InvokeRepeating("GenerateObjects", spawnTime, spawnRate);
    }

    private void GenerateObjects()
    {
        for (int i = 0; i < objectsToSpawn.Length; i++)
        {
            int randomIndex = Random.Range(0, spawnLocations.Length);
            Vector2 randomLocation = spawnLocations[randomIndex].position;

            // Determinar la dirección de movimiento en función de la posición
            Vector2 moveDirection = Vector2.right; // Predeterminado a derecha
            if (randomLocation.x < leftBoundary.position.x)
            {
                moveDirection = Vector2.right; // Mover hacia la derecha
            }
            else if (randomLocation.x > rightBoundary.position.x)
            {
                moveDirection = Vector2.left; // Mover hacia la izquierda
            }

            // Instanciar objeto y asignar la dirección de movimiento
            GameObject spawnedObject = Instantiate(objectsToSpawn[i], randomLocation, Quaternion.identity);
            Enemigo enemigoScript = spawnedObject.GetComponent<Enemigo>();
            if (enemigoScript != null)
            {
                enemigoScript.SetMoveDirection(moveDirection);
            }
        }
    }
}
