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
        // Press Escape / Back button to toggle panel
        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePausePanel();
    }

    public void TogglePausePanel()
    {
        if (isPaused)
            ResumeGame();
        else
            PauseGame();
    }

    public void PauseGame()
    {
        isPaused = true;
        if (pausePanel != null)
            pausePanel.SetActive(true);

        Time.timeScale = 0f; // freeze the game
        AudioListener.pause = true; // optional: pause all audio
    }

    public void ResumeGame()
    {
        isPaused = false;
        if (pausePanel != null)
            pausePanel.SetActive(false);

        Time.timeScale = 1f; // resume game
        AudioListener.pause = false;
    }
}
