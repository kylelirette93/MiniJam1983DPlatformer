using UnityEngine;

public class Rotate : MonoBehaviour
{
    private void Update()
    {
        // Rotate all axis 45 degrees per second.
        transform.Rotate(new Vector3(45, 45, 45) * Time.deltaTime);
    }
}
