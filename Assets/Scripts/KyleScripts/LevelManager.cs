using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Transform player;
    public void LoadScene(int sceneIndex)
    {
        // Load scene by index and subscribe to sceneLoaded event.
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(sceneIndex);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        GameObject playerObj = GameObject.Find("Player");
        PlayerController playerController = playerObj.GetComponent<PlayerController>();

        Transform leftLane = GameObject.Find("LeftLane").transform;
        Transform centerLane = GameObject.Find("CenterLane").transform;
        Transform rightLane = GameObject.Find("RightLane").transform;

        playerController.SetLanes(leftLane, centerLane, rightLane);

        GameObject spawnPoint = GameObject.Find("SpawnPoint");
        if (playerObj != null && spawnPoint != null)
        {
            playerObj.transform.position = spawnPoint.transform.position;
        }
        else
        {
            Debug.LogWarning("Player or SpawnPoint not found in the scene.");
        }
    }
}
