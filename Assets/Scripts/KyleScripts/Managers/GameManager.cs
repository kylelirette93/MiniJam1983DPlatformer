using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private GameStateManager gameStateManager;
    private UIManager uiManager;
    private AudioManager audioManager;
    private LevelManager levelManager;

    public GameStateManager GameStateManager => gameStateManager;
    public UIManager UIManager => uiManager;
    public AudioManager AudioManager => audioManager;
    public LevelManager LevelManager => levelManager;
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
        uiManager = GetComponentInChildren<UIManager>();
        audioManager = GetComponentInChildren<AudioManager>();
        levelManager = GetComponentInChildren<LevelManager>();
        #endregion
    }
}
