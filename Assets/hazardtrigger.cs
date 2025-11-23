using UnityEngine;

public class hazardtrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            // This is where we do stuff.
            // Pickup recharges stamina
            Stamina.Increase(15f);
            Destroy(gameObject);
        }
    }
}
