using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private GameStateManager GameStateManager;
    private UIManager UIManager;
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
        GameStateManager = GetComponentInChildren<GameStateManager>();
        UIManager = GetComponentInChildren<UIManager>();
        #endregion
    }
}
