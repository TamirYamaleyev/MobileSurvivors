using UnityEngine;
using TMPro;

public class ScoreHUD : MonoBehaviour
{
    public TMP_Text Score;   // assign in inspector
    private int score = 0;

    void Start()
    {
        UpdateScoreHUD();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreHUD();
    }

    private void UpdateScoreHUD()
    {
        Score.text = "Score: " + score;
    }
}