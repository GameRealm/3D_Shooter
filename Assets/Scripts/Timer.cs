using UnityEngine;
using TMPro;
using System.Collections; 

public class Timer : MonoBehaviour
{
    public float time = 90f;
    public TextMeshProUGUI timetext;

    [Header("Налаштування звуку")]
    public AudioSource audioSource;   
    public AudioClip beepSound;     

    private bool isEndingSequenceStarted = false; 

    void Start()
    {
        Time.timeScale = 1f;

        if (audioSource == null) audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
            UpdateTimerDisplay();

            if (time <= 5f && !isEndingSequenceStarted)
            {
                StartCoroutine(PlayCountdownBeeps());
            }
        }
        else
        {
            time = 0;
            timetext.text = "0:00";

            if (StatisticsManager.instance != null)
            {
                StatisticsManager.instance.ShowStatistics();
            }
        }
    }

    IEnumerator PlayCountdownBeeps()
    {
        isEndingSequenceStarted = true;

        for (int i = 0; i < 5; i++)
        {
            if (time <= 0) break; 

            if (audioSource != null && beepSound != null)
            {

                audioSource.pitch = 1f + (i * 0.1f);
                audioSource.PlayOneShot(beepSound);
            }

            timetext.color = Color.red;

            yield return new WaitForSeconds(1f);
        }

        if ( audioSource != null)
        {
            audioSource.pitch = 1f; 
        }
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);

        if (minutes >= 1)
        {
            timetext.text = string.Format("{0}:{1:00}", minutes, seconds);
        }
        else
        {
            timetext.text = seconds.ToString();

            if (time <= 5f)
            {
                float scale = 1f + Mathf.PingPong(Time.time * 5f, 0.2f);
                timetext.transform.localScale = new Vector3(scale, scale, 1f);
            }
        }
    }
}