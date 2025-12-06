using UnityEngine;
using UnityEngine.UI;

public class UiLevelDisplay : MonoBehaviour
{
    public PlayerXp playerXp;  
    public Text levelText;     // reference to UI text

    void Start()
    {
        UpdateLevelText();
    }

    public void UpdateLevelText()
    {
        if (levelText != null)
            levelText.text = "Level " + playerXp.level;
    }
}