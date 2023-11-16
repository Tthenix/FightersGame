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
         else if(other.CompareTag("Bala1"))
         {
            Debug.Log("La bala 2 colisionó con otra bala");
            Destroy(gameObject);
            Destroy(other.gameObject); // Destroy the other bullet here
         }
   }
}
