using TMPro;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    public TextMeshProUGUI numberOneScore;
    public TextMeshProUGUI numberTwoScore;
    public TextMeshProUGUI numberThreeScore;

    public TextMeshProUGUI scoreThisRound;

    public TMP_InputField initialsInput;
    public GameObject initialsInputPanel;

    float currentScore;
    bool isNewHighScore = false;
    private void OnEnable()
    {
        PlayerPrefs.GetFloat("HighSpeed1", 0);
        PlayerPrefs.GetFloat("HighSpeed2", 0);
        PlayerPrefs.GetFloat("HighSpeed3", 0);

        PlayerPrefs.GetString("Initials1", "AAA");
        PlayerPrefs.GetString("Initials2", "AAA");
        PlayerPrefs.GetString("Initials3", "AAA");
        currentScore = Speed.GetSpeed();
        CheckIfHighscore();
        UpdateLeaderBoard();
    }

    private void Start()
    {
        // Add listener to input field
        initialsInput.onEndEdit.AddListener(OnInitialsEntered);
    }

    private void CheckIfHighscore()
    {
        float highScore3 = PlayerPrefs.GetFloat("HighSpeed3", 0);
        isNewHighScore = currentScore > highScore3;

        // Hide or show initials based on if its a new high score.
        if (initialsInputPanel != null)
        {
            initialsInputPanel.SetActive(isNewHighScore);
        }
        else
        {
            initialsInput.gameObject.SetActive(isNewHighScore);
        }

        if (isNewHighScore)
        {
            initialsInput.interactable = true;
            initialsInput.text = "";
            initialsInput.ActivateInputField(); 
        }
    }

    private void OnInitialsEntered(string input)
    {
        if (isNewHighScore && !string.IsNullOrEmpty(input))
        {
            string initials = input.ToUpper();
            if (initials.Length > 3) initials = initials.Substring(0, 3);

            UpdateTop3("HighSpeed", "Initials", currentScore, initials);
            DisplayLeaderboard();

            // Disable input after player submit.
            initialsInput.interactable = false;
        }
    }

    public void UpdateLeaderBoard()
    {
        scoreThisRound.text = "Max Speed: " + Mathf.RoundToInt(currentScore);
        DisplayLeaderboard();
    }

    public void UpdateTop3(string prefix, string initialsPrefix, float score, string initials)
    {
        float highScore1 = PlayerPrefs.GetFloat(prefix + "1", 0);
        float highScore2 = PlayerPrefs.GetFloat(prefix + "2", 0);
        float highScore3 = PlayerPrefs.GetFloat(prefix + "3", 0);

        if (score > highScore1)
        {
            PlayerPrefs.SetFloat(prefix + "3", highScore2);
            PlayerPrefs.SetFloat(prefix + "2", highScore1);
            PlayerPrefs.SetFloat(prefix + "1", score);

            PlayerPrefs.SetString(initialsPrefix + "3", PlayerPrefs.GetString(initialsPrefix + "2", "AAA"));
            PlayerPrefs.SetString(initialsPrefix + "2", PlayerPrefs.GetString(initialsPrefix + "1", "AAA"));
            PlayerPrefs.SetString(initialsPrefix + "1", initials);
        }
        else if (score > highScore2)
        {
            PlayerPrefs.SetFloat(prefix + "3", highScore2);
            PlayerPrefs.SetFloat(prefix + "2", score);

            PlayerPrefs.SetString(initialsPrefix + "3", PlayerPrefs.GetString(initialsPrefix + "2", "AAA"));
            PlayerPrefs.SetString(initialsPrefix + "2", initials);
        }
        else if (score > highScore3)
        {
            PlayerPrefs.SetFloat(prefix + "3", score);
            PlayerPrefs.SetString(initialsPrefix + "3", initials);
        }

        PlayerPrefs.Save();
    }

    private void DisplayLeaderboard()
    {
        float speed1 = PlayerPrefs.GetFloat("HighSpeed1", 0);
        string initials1 = PlayerPrefs.GetString("Initials1", "AAA");
        numberOneScore.text = $"1. {initials1} - Max Speed: {Mathf.RoundToInt(speed1)}";

        float speed2 = PlayerPrefs.GetFloat("HighSpeed2", 0);
        string initials2 = PlayerPrefs.GetString("Initials2", "AAA");
        numberTwoScore.text = $"2. {initials2} - Max Speed: {Mathf.RoundToInt(speed2)}";

        float speed3 = PlayerPrefs.GetFloat("HighSpeed3", 0);
        string initials3 = PlayerPrefs.GetString("Initials3", "AAA");
        numberThreeScore.text = $"3. {initials3} - Max Speed: {Mathf.RoundToInt(speed3)}";
    }
}
    
