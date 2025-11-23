using UnityEngine;

/// <summary>
/// UI Manager handles showing and hiding different UI panels.
/// </summary>
public class UIManager : MonoBehaviour
{
    #region UI Panels
    public GameObject MainMenuPanel;
    public GameObject GameplayPanel;
    public GameObject PauseMenuPanel;
    public GameObject GameOverPanel;
    public SlidingSprite slidingSprite;
    #endregion

    /// <summary>
    /// Disable all menu panels. Called before enabling a specific panel.
    /// </summary>
    private void DisableAllMenuUI()
    {
        #region Deactivate All Panels
        // Disable all UI panels.
        MainMenuPanel.SetActive(false);
        GameplayPanel.SetActive(false);
        PauseMenuPanel.SetActive(false);
        GameOverPanel.SetActive(false);
        #endregion
    }

    #region Show Specific UI Panel Methods
    public void ShowMainMenuUI()
    {
        DisableAllMenuUI();
        MainMenuPanel.SetActive(true);
        slidingSprite.gameObject.SetActive(true);
    }
    public void ShowGameplayUI()
    {
        DisableAllMenuUI();
        GameplayPanel.SetActive(true);
        slidingSprite.gameObject.SetActive(false);
    }
    public void ShowPauseMenuUI()
    {
        DisableAllMenuUI();
        PauseMenuPanel.SetActive(true);
        slidingSprite.gameObject.SetActive(false);
    }
    public void ShowGameOverUI()
    {
        DisableAllMenuUI();
        GameOverPanel.SetActive(true);
        slidingSprite.gameObject.SetActive(true);
    }
    #endregion
}
