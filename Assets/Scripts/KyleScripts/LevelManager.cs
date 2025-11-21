using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Splines;

public class LevelManager : MonoBehaviour
{
    public GameObject player;
    public void LoadScene(int sceneIndex)
    {
        // Load scene by index and subscribe to sceneLoaded event.
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(sceneIndex);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        //Debug.Log("Scene Loaded: " + scene.name);

        PlayerController playerController = player.GetComponent<PlayerController>();

        GameObject spawnPoint = GameObject.Find("SpawnPoint");
        if (player != null && spawnPoint != null)
        {
            player.transform.position = spawnPoint.transform.position;
            player.transform.forward = spawnPoint.transform.forward;
        }

        GameObject trackParent = GameObject.Find("trackv2");

        if (trackParent != null)
        {
            SplineContainer splineContainer = trackParent.GetComponent<SplineContainer>();
            Transform leftLaneObj = trackParent.transform.Find("TrackLane1");
            Transform centerLaneObj = trackParent.transform.Find("TrackLane2");
            Transform rightLaneObj = trackParent.transform.Find("TrackLane3");

            if (leftLaneObj != null && centerLaneObj != null && rightLaneObj != null)
            {
                SplineContainer leftLane = leftLaneObj.GetComponent<SplineContainer>();
                SplineContainer centerLane = centerLaneObj.GetComponent<SplineContainer>();
                SplineContainer rightLane = rightLaneObj.GetComponent<SplineContainer>();
                playerController.SetLanes(leftLane, centerLane, rightLane);
                Debug.Log("Lanes set successfully!");
            }
            else
            {
                Debug.LogError("One or more lanes not found as children!");
            }
        }
        else
        {
            Debug.LogError("Track parent not found!");
        }
    }
}
