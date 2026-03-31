using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Обов'язково для керування сценами

public class UI : MonoBehaviour
{
    // Метод для кнопки "Старт" або "Почати гру"
    public void StartGame()
    {
        // Отримуємо індекс поточної сцени та додаємо 1
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        // Перевіряємо, чи існує наступна сцена в списку Build Settings
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("Наступної сцени не існує в Build Settings!");
        }
    }

    // Метод для кнопки "Вихід"
    public void OnExitButton()
    {
        Debug.Log("Вихід з гри...");

        // Працює у зібраній грі (.exe, .app)
        Application.Quit();

        // Якщо ти тестуєш у самому редакторі Unity, додаємо це:
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}