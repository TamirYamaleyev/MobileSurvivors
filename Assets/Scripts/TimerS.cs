using UnityEngine;
using TMPro;
using System.Collections;

public class LevelTimerHUD : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text Timer; // assign in inspector

    [Header("Settings")]
    public float flashDuration = 0.15f;
    public Color flashColor = Color.yellow; // flash every minute

    private float elapsedTime = 0f;
    private Color originalColor;
    private Coroutine flashRoutine;

    void Start()
    {
        originalColor = Timer.color;
        Timer.enableAutoSizing = false; // Important
        UpdateTimerDisplay(0);
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        UpdateTimerDisplay(elapsedTime);

        // Flash at every full minute (60, 120, 180…)
        if (Mathf.Approximately(elapsedTime % 60f, Time.deltaTime))
        {
            if (flashRoutine != null)
                StopCoroutine(flashRoutine);
            flashRoutine = StartCoroutine(ColorFlash());
        }
    }

    private void UpdateTimerDisplay(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        int milliseconds = Mathf.FloorToInt((time * 1000f) % 1000);

        // Format: MM:SS:MMM
        Timer.text = $"{minutes:00}:{seconds:00}";
    }

    private IEnumerator ColorFlash()
    {
        float timer = 0f;

        // Fade to flash color
        while (timer < flashDuration)
        {
            timer += Time.deltaTime;
            float t = timer / flashDuration;
            Timer.color = Color.Lerp(originalColor, flashColor, t);
            yield return null;
        }

        timer = 0f;

        // Fade back to original color
        while (timer < flashDuration)
        {
            timer += Time.deltaTime;
            float t = timer / flashDuration;
            Timer.color = Color.Lerp(flashColor, originalColor, t);
            yield return null;
        }

       Timer.color = originalColor;
    }

    public void SetElapsedTime(float time)
    {
        elapsedTime = time;
        UpdateTimerDisplay(time);
    }

    public float ElapsedTime => elapsedTime;
}
