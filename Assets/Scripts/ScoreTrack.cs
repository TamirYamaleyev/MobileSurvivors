using UnityEngine;
using TMPro;

public class ScoreHUD : MonoBehaviour
{
    public TMP_Text Score;   // assign in inspector

    void Start()
    {
        UpdateScoreHUD(0);
    }

    public void UpdateScoreHUD(int score)
    {
        Score.text = "Score:\n" + score;
    }
}