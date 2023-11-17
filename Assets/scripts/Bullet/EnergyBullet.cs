using UnityEngine;

public class EnergyBullet : MonoBehaviour
{
   [SerializeField] private float velocidad;
   [SerializeField] private float daño;

   private void Update()
   {
      transform.Translate(Vector2.right * velocidad * Time.deltaTime);
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
         if(other.gameObject.CompareTag("Jugador2"))
         {
            other.GetComponent<Player2>().TomarDaño(daño);
            Destroy(gameObject);
         }

 
   }

   void OnCollisionEnter2D(Collision2D other)
    {
         if (other.gameObject.CompareTag("Bala2"))
         {
            Debug.Log("La bala 2 colisionó con otra bala");
            Destroy(gameObject);
            Destroy(other.gameObject); // Destroy the other bullet here
            Destroy(this.gameObject);
         }
    }
}
