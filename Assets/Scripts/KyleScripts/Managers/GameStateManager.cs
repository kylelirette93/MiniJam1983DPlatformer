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
    PlayerController PlayerController => GameManager.Instance.PlayerController;

    /// <summary>
    /// Handle switching between game states, storing previous state.
    /// </summary>
    /// <param name="newState">The state to switch to.</param>
    public void SwitchToState(GameState newState)
    {

        // Exit previous state
        OnExitState(previousState);

        previousState = currentState;
        currentState = newState;

        // Enter new state
        OnEnterState(newState);
    }

    private void OnEnterState(GameState state)
    {
        Debug.Log($"ENTERING STATE: {state}");
        switch (state)
        {
            case GameState.MainMenu:
                UIManager.ShowMainMenuUI();
                break;
            case GameState.Tutorial:
                UIManager.ShowTutorialUI();
                break;
            case GameState.Gameplay:
                UIManager.ShowGameplayUI();
                Time.timeScale = 1f;
                PlayerController.ResetPlayerStats();
                PlayerController.StartMoving();
                break;
            case GameState.GameOver:
                UIManager.ShowGameOverUI();
                break;
            case GameState.Paused:
                UIManager.ShowPauseMenuUI();
                Time.timeScale = 0f;
                break;
        }
    }

    private void OnExitState(GameState state)
    {
        // Clean up if needed
    }

    public void Update()
    {
        switch (currentState)
        {
            case GameState.Gameplay:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    PauseGame();
                }
                break;
            case GameState.Paused:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    ResumeGame();
                }
                break;
        }
    }

    public void StartTutorial()
    {
        LevelManager.LoadScene(1); // Assuming scene index 1 is the gameplay scene.
        SwitchToState(GameState.Tutorial);
    }

    /// <summary>
    /// Play game button, loads first level and switches state.
    /// </summary>
    public void PlayGame()
    {
        Debug.Log("Play game clicked!");
        SwitchToState(GameState.Gameplay);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
        if (currentState == GameState.Gameplay)
        {
            SwitchToState(GameState.Paused);
        }
    }

    public void ResumeGame()
    {
        if (currentState == GameState.Paused) 
        {
            SwitchToState(GameState.Gameplay);
        }
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
    Tutorial,
    Gameplay,
    GameOver,
    Paused
}
