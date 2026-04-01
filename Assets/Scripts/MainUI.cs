using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [Header("Налаштування звуку")]
    public AudioSource audioSource;  
    public AudioClip clickSound;   

    private void PlayClickSound()
    {
        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }

    public void StartGame()
    {
        PlayClickSound();
        Invoke("LoadNextScene", 0.2f);
    }

    private void LoadNextScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("Наступної сцени не існує!");
        }
    }

    public void OnExitButton()
    {
        PlayClickSound();
        Application.Quit();
    }
}