using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private GameState previousState;
    [SerializeField] private GameState currentState;

    UIManager UIManager => GameManager.Instance.UIManager;
    LevelManager LevelManager => GameManager.Instance.LevelManager;

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

    public void PlayGame()
    {
        LevelManager.LoadScene(1); // Assuming scene index 1 is the gameplay scene.
        SwitchToState(GameState.Gameplay);
    }
}

public enum GameState
{
    MainMenu,
    Gameplay,
    Credits,
    GameOver,
    Paused
}
