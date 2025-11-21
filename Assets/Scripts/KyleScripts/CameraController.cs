using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    Transform player;
    [SerializeField] Vector3 offset = new Vector3(0f, 5f, -10f);

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
            Vector3 desiredPosition = player.position + player.rotation * offset;

            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * 5f);

            transform.LookAt(player.position + Vector3.up * 2f);
        }
    }
}
