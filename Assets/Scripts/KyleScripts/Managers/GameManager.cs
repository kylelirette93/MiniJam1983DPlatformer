using UnityEngine;

/// <summary>
/// Game Manager that holds reference to all other managers. For ease of access.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // Private ref to each manager.
    private GameStateManager gameStateManager;
    private UIManager uiManager;
    private AudioManager audioManager;
    private LevelManager levelManager;
    private PlayerController playerController;

    // Public getters for each manager.
    public GameStateManager GameStateManager => gameStateManager;
    public UIManager UIManager => uiManager;
    public AudioManager AudioManager => audioManager;
    public LevelManager LevelManager => levelManager;
    public PlayerController PlayerController => playerController;
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
        // Each manager is a child of Game Manager. Here I'm grabbing ref to each.
        gameStateManager = GetComponentInChildren<GameStateManager>();
        uiManager = GetComponentInChildren<UIManager>();
        audioManager = GetComponentInChildren<AudioManager>();
        levelManager = GetComponentInChildren<LevelManager>();
        playerController = GetComponentInChildren<PlayerController>();
        #endregion
    }
}
