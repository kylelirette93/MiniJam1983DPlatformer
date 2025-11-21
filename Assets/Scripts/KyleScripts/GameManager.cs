using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private GameStateManager gameStateManager;
    private void Awake()
    {
        #region Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        #endregion

        #region References
        gameStateManager = GetComponentInChildren<GameStateManager>();
        #endregion
    }
}
