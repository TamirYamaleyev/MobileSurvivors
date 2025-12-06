using UnityEngine;

public class PlayerXp : MonoBehaviour
{
    public float currentXp = 0f;
    public float maxXp = 100f;

    public GettingXp xpBar; // reference to UI bar
    public UiLevelDisplay levelText;

    public int level = 1;  

    void Start()
    {
        if (xpBar != null)
        xpBar.UpdateXpBar(currentXp, maxXp);
        
        if (levelText != null)
        levelText.UpdateLevelText();
    }

    public void AddXp (float amount)
    {
        currentXp += amount;

        // If XP reaches or exceeds the bar end it levels up
        if (currentXp >= maxXp)
        {
            currentXp -= maxXp;  
            LevelUp();
        }

        xpBar.UpdateXpBar(currentXp, maxXp);
    }

    void LevelUp()
    {
        level++;
        Debug.Log("LEVEL UP! New Level: " + level);
        levelText.UpdateLevelText();

        xpBar.UpdateXpBar(currentXp, maxXp);
        
    }
}