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
    public GameObject CreditsPanel;
    public GameObject GameOverPanel;
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
        CreditsPanel.SetActive(false);
        GameOverPanel.SetActive(false);
        #endregion
    }

    #region Show Specific UI Panel Methods
    public void ShowMainMenuUI()
    {
        DisableAllMenuUI();
        MainMenuPanel.SetActive(true);
    }
    public void ShowGameplayUI()
    {
        DisableAllMenuUI();
        GameplayPanel.SetActive(true);
    }
    public void ShowPauseMenuUI()
    {
        DisableAllMenuUI();
        PauseMenuPanel.SetActive(true);
    }
    public void ShowCreditsUI()
    {
        DisableAllMenuUI();
        CreditsPanel.SetActive(true);
    }
    public void ShowGameOverUI()
    {
        DisableAllMenuUI();
        GameOverPanel.SetActive(true);
    }
    #endregion
}
