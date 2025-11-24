using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    [Header("UI Panel")]
    public GameObject pausePanel; // Assign your SaveLoadPanel here

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
        if (pausePanel != null)
            pausePanel.SetActive(!pausePanel.activeSelf);
    }

    public void ShowPausePanel()
    {
        if (pausePanel != null)
            pausePanel.SetActive(true);
    }

    public void HidePausePanel()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);
    }
}
