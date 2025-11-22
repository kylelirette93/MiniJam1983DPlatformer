using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Game State Manager to handle switching between different game states.
/// </summary>
public class GameStateManager : MonoBehaviour
{
    [Header("Game State Info")]
    [SerializeField] private GameState previousState;
    [SerializeField] private GameState currentState;

    // References to what we need via service locator pattern.
    UIManager UIManager => GameManager.Instance.UIManager;
    LevelManager LevelManager => GameManager.Instance.LevelManager;

    /// <summary>
    /// Handle switching between game states, storing previous state.
    /// </summary>
    /// <param name="newState">The state to switch to.</param>
    public void SwitchToState(GameState newState)
    {
        previousState = currentState;
        currentState = newState;
    }

    public void Update()
    {
        switch (currentState)
        {
            case GameState.MainMenu:
                UIManager.ShowMainMenuUI();
                break;
            case GameState.Gameplay:
                UIManager.ShowGameplayUI();
                break;
            case GameState.Credits:
                UIManager.ShowCreditsUI();
                break;
            case GameState.GameOver:
                UIManager.ShowGameOverUI();
                break;
            case GameState.Paused:
                UIManager.ShowPauseMenuUI();
                break;
        }
    }

    /// <summary>
    /// Play game button, loads first level and switches state.
    /// </summary>
    public void PlayGame()
    {
        LevelManager.LoadScene(1); // Assuming scene index 1 is the gameplay scene.
        SwitchToState(GameState.Gameplay);
    }

    public void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            LevelManager.LoadScene(currentSceneIndex + 1);
        }
        else
        {
            // Go back to first level if no more levels.
            LevelManager.LoadScene(1);
        }
    }
}

/// <summary>
/// Game State enum for different states the game can be in.
/// </summary>
public enum GameState
{
    MainMenu,
    Gameplay,
    Credits,
    GameOver,
    Paused
}
