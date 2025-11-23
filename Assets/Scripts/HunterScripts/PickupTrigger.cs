using UnityEngine;

public class PickupTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // This is where we do stuff.
            // Pickup recharges stamina
            GameManager.Instance.AudioManager.PlaySFX("Pickup", 1);
            Stamina.Increase(15f);
            Destroy(gameObject);
        }
    }
}
