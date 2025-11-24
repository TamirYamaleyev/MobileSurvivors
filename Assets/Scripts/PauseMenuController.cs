using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    [Header("UI Panel")]
    public GameObject pausePanel; // Assign your SaveLoadPanel here

    private bool isPaused = false;

    void Start()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false); // Start hidden
    }

    void Update()
    {
        // Press Escape / Back button to toggle panel for testing
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePausePanel();
        }
    }

    public void TogglePausePanel()
    {
        if (pausePanel == null) return;

        isPaused = !isPaused;
        pausePanel.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f; // Pause or resume
    }

    public void ShowPausePanel()
    {
        if (pausePanel == null) return;

        isPaused = true;
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void HidePausePanel()
    {
        if (pausePanel == null) return;

        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
