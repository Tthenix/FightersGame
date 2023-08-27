using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] float timeSpawn;
    float timerCounter;
    bool canSpawn;

    [SerializeField] BulletMovement bullet;

    private void Update()
    {
        timerCounter += Time.deltaTime;
        if (timerCounter > timeSpawn)
        {
            timerCounter = 0;
            canSpawn = true;
        }
    }

    private void FixedUpdate()
    {
        if (canSpawn)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject go = Instantiate(bullet.gameObject, transform.position, Quaternion.identity);
        canSpawn = false;
    }

}
