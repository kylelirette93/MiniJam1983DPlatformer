using UnityEngine;

/// <summary>
/// Gizmo class to visualize an object in scene mode.
/// </summary>
public class Gizmo : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.5f);
    }
}
