using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TextMeshProUGUI scoreText;
    [HideInInspector] public int totalScore = 0;
    [HideInInspector] public int simpleCount = 0;
    [HideInInspector] public int mediumCount = 0;
    [HideInInspector] public int hardCount = 0;
    [HideInInspector] public int bonusesCollected = 0;

    private void Awake() => instance = this;

    public void AddScore(int points, string type)
    {
        totalScore += points;
        if (type == "Simple") simpleCount++;
        else if (type == "Medium") mediumCount++;
        else if (type == "Hard") hardCount++;

        UpdateUI();
    }

    public void AddBonusCount() => bonusesCollected++;

    private void UpdateUI() => scoreText.text = "Score: " + totalScore;

    public int GetHighScore()
    {
        int saved = PlayerPrefs.GetInt("HighScore", 0);
        if (totalScore > saved)
        {
            PlayerPrefs.SetInt("HighScore", totalScore);
            return totalScore;
        }
        return saved;
    }
}