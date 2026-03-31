using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StatisticsManager : MonoBehaviour
{
    public static StatisticsManager instance;

    [Header("UI Елементи")]
    public GameObject endMenuUI;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI bonusStatsText;
    public TextMeshProUGUI detailedTargetsText;

    [Header("Поле для фінальної цитати")]
    public TextMeshProUGUI quoteText; 

    private void Awake() => instance = this;

    public void ShowStatistics()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        endMenuUI.SetActive(true);

        var sm = ScoreManager.instance;

        finalScoreText.text = " " + sm.totalScore;
        highScoreText.text = "Best score: " + sm.GetHighScore();
        bonusStatsText.text = "Bonuses earned: " + sm.bonusesCollected;

        detailedTargetsText.text =
            $"Simple targets: {sm.simpleCount}\n" +
            $"Medium targets: {sm.mediumCount}\n" +
            $"Hard targets: {sm.hardCount}";

        SetFinalQuote(sm.totalScore);

        if (sm.totalScore >= PlayerPrefs.GetInt("HighScore") && sm.totalScore > 0)
            finalScoreText.text += " <color=yellow>(РЕКОРД!)</color>";
    }

    private void SetFinalQuote(int score)
    {
        string quote = "";

        if (score <= 0)
            quote = "Were you really trying to hit the target? Or are you on the targets' side?";
        else if (score < 20)
            quote = "Not bad for a beginner, but the island has seen better shooters.";
        else if (score < 50)
            quote = "Your skills are improving! The targets are already starting to fear you.";
        else if (score < 100)
            quote = "You're a true hunter. The island has been conquered!";
        else
            quote = "Legendary! Your name will be carved on every stone on this island.";

        quoteText.text = $"\"{quote}\"";
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void LoadPreviousScene()
    {
        int previousSceneIndex = SceneManager.GetActiveScene().buildIndex - 1;
        Time.timeScale = 1f;
        SceneManager.LoadScene(previousSceneIndex);
    }
}