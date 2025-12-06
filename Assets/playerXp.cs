using UnityEngine;
using UnityEngine.UI;
#if TMP_PRESENT
using TMPro;
#endif

public class PlayerXP : MonoBehaviour
{
    [Header("XP Stats")]
    public int level = 1;
    public int currentXP = 0;
    public int xpToNextLevel = 100;

    [Header("UI")]
    public Slider xpSlider;
#if TMP_PRESENT
    public TextMeshProUGUI xpText;
#else
    public Text xpTextLegacy;
#endif

    private void Start()
    {
        UpdateUI();
    }

    // Call this when the player picks up XP shards
    public void AddXP(int amount)
    {
        currentXP += amount;

        // Handle level up
        while (currentXP >= xpToNextLevel)
        {
            currentXP -= xpToNextLevel;
            LevelUp();
        }

        UpdateUI();
    }

    private void LevelUp()
    {
        level++;
        xpToNextLevel = Mathf.RoundToInt(xpToNextLevel * 1.15f); // Progressive increase
        Debug.Log("LEVEL UP! New Level: " + level);
    }

    private void UpdateUI()
    {
        if (xpSlider != null)
        {
            xpSlider.maxValue = xpToNextLevel;
            xpSlider.value = currentXP;
        }

#if TMP_PRESENT
        if (xpText != null)
            xpText.text = $"Level {level} — {currentXP}/{xpToNextLevel} XP";
#else
        if (xpTextLegacy != null)
            xpTextLegacy.text = $"Level {level} — {currentXP}/{xpToNextLevel} XP";
#endif
    }
}