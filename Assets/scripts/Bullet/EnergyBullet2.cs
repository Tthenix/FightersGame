using UnityEngine;

public class EnergyBullet2 : MonoBehaviour
{
   [SerializeField] private float velocidad;
   [SerializeField] private float daño;

   private void Update()
   {
      transform.Translate(Vector2.left * velocidad * Time.deltaTime);
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
         if(other.CompareTag("Jugador1"))
         {
            other.GetComponent<Player>().TomarDaño(daño);
            Destroy(gameObject);
         }
   }
}
