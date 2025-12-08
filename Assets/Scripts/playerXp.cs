using UnityEngine;

public class PlayerXp : MonoBehaviour
{
    [SerializeField]
    private float currentXp = 0f;
    public float maxXp = 100f;

    public GettingXp xpBar; // reference to UI bar
    public UiLevelDisplay levelText;

    public int level = 1;  

    void Start()
    {
        if (xpBar != null)
            xpBar.UpdateXpBar(currentXp, maxXp);
        
        if (levelText != null)
            levelText.UpdateLevelText(level);
    }

    public void AddXp (float amount)
    {
        currentXp += amount;

        // If XP reaches or exceeds the bar end it levels up
        if (currentXp >= maxXp)
        {
            currentXp = 0;
            maxXp *= 1.1f;

            LevelUp();
        }

        xpBar.UpdateXpBar(currentXp, maxXp);
    }

    void LevelUp()
    {
        level++;

        Debug.Log("LEVEL UP! New Level: " + level);
        levelText.UpdateLevelText(level);

        xpBar.UpdateXpBar(currentXp, maxXp);
    }
}