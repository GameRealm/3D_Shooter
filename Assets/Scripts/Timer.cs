using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float time = 90f; // Наприклад, 90 секунд (1:30)
    public TextMeshProUGUI timetext;
    void Start()
    {
        Time.timeScale = 1f; // Дублюємо тут для надійності
    }
    void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
            UpdateTimerDisplay();
        }
        else
        {
            time = 0;
            timetext.text = "0:00";

            // Викликаємо статистику
            if (StatisticsManager.instance != null)
            {
                // Беремо бали з ScoreManager
                int finalScore = 0;
                // Припускаємо, що в ScoreManager є public int totalScore
                finalScore = ScoreManager.instance.totalScore; 

                StatisticsManager.instance.ShowStatistics();
            }
        }
    }

    void UpdateTimerDisplay()
    {
        // Розраховуємо хвилини та секунди
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);

        if (minutes >= 1)
        {
            // Якщо є хоча б одна хвилина, виводимо формат "М:СС"
            // string.Format("{0}:{1:00}") зробить так, щоб 1 хв 5 сек виглядало як 1:05, а не 1:5
            timetext.text = string.Format("{0}:{1:00}", minutes, seconds);
        }
        else
        {
            // Якщо менше хвилини — виводимо тільки секунди
            timetext.text = seconds.ToString();
        }
    }
}