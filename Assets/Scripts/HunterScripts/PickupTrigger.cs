using UnityEngine;

public class PickupTrigger : MonoBehaviour
{
    [SerializeField] ParticleSystem particles;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Vector3 spawnPosition = transform.position;
            // This is where we do stuff.
            // Pickup recharges stamina
            GameManager.Instance.AudioManager.PlaySFX("Pickup", 1);
            Stamina.Increase(15f);
            GameObject temp = Instantiate(particles.gameObject, spawnPosition, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
