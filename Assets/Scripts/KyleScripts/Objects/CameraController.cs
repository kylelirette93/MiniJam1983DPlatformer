using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Camera controller to smoothly follow player.
/// </summary>
public class CameraController : MonoBehaviour
{
    Transform player;

    [Header("Camera Settings")]
    [SerializeField] Vector3 cameraOffset = new Vector3(0f, 5f, -10f);

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        // Grab reference to player when a new scene is loaded
        GameObject playerObj = GameObject.Find("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    private void LateUpdate()
    {
        if (player != null)
        {
            // Determine desired position based on player position and camera offset.
            Vector3 desiredPosition = player.position + player.rotation * cameraOffset;

            // Smoothly interpolate to desired position.
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * 5f);

            // Look at the player with a slight upward offset.
            transform.LookAt(player.position + Vector3.up * 1.2f);
        }
    }
}
