using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private GameState previousState;
    [SerializeField] private GameState currentState;

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
                // Handle main menu logic.
                break;
            case GameState.Gameplay:
                // Handle gameplay logic.
                break;
            case GameState.Credits:
                // Handle credits logic.
                break;
            case GameState.GameOver:
                // Handle game over logic.
                break;
            case GameState.Paused:
                // Handle pause logic.
                break;
        }
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
