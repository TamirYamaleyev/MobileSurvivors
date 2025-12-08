using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiLevelDisplay : MonoBehaviour
{ 
    public TextMeshProUGUI levelText;     // reference to UI text

    void Start()
    {
        UpdateLevelText(1);
    }

    public void UpdateLevelText(int level)
    {
        if (levelText != null)
            levelText.text = "LV: " + level;
    }
}