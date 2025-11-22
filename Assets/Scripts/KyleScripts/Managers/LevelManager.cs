using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Splines;

/// <summary>
/// Level Manager handles loading scenes and initializing everything needed when a new level is loaded.
/// </summary>
public class LevelManager : MonoBehaviour
{
    public GameObject player;

    /// <summary>
    /// Loads a scene by a given index.
    /// </summary>
    /// <param name="sceneIndex">The build index of scene to load.</param>
    public void LoadScene(int sceneIndex)
    {
        // Load scene by index and subscribe to sceneLoaded event.
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(sceneIndex);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Unsubscribe from scene loaded to avoid multiple calls.
        SceneManager.sceneLoaded -= OnSceneLoaded;
        //Debug.Log("Scene Loaded: " + scene.name);

        #region Move Player to Spawn Point
        PlayerController playerController = player.GetComponent<PlayerController>();

        // Grab spawn point in scene and set player pos and rot to it.
        GameObject spawnPoint = GameObject.Find("SpawnPoint");
        if (player != null && spawnPoint != null)
        {
            player.transform.position = spawnPoint.transform.position;
            player.transform.forward = spawnPoint.transform.forward;
        }
        #endregion

        #region Pass Spline Track to Player
        // Grab center spline track from scene.
        GameObject trackParent = GameObject.Find("trackv2");

        if (trackParent != null)
        {
            SplineContainer splineContainer = trackParent.GetComponent<SplineContainer>();
            Transform centerLaneObj = trackParent.transform.Find("CenterLane");

            if (centerLaneObj != null)
            {
                SplineContainer centerLane = centerLaneObj.GetComponent<SplineContainer>();
                playerController.SetLanes(centerLane);
                Debug.Log("Lanes set successfully!");
            }
            else
            {
                Debug.LogError("Center lane not found");
            }
        }
        else
        {
            Debug.LogError("Track parent not found!");
        }
        #endregion
    }
}
