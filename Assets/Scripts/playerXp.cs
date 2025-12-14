using UnityEngine;

public class PlayerXp : MonoBehaviour
{
    [SerializeField]
    private float currentXp = 0f;
    public float maxXp = 100f;

    public GettingXp xpBar; // reference to UI bar
    public UiLevelDisplay levelText;

    PlayerController playerContr;

    public int level = 1;  

    void Start()
    {
        playerContr = GetComponent<PlayerController>();

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

    private void LevelUp()
    {
        level++;

        Debug.Log("LEVEL UP! New Level: " + level);
        levelText.UpdateLevelText(level);

        xpBar.UpdateXpBar(currentXp, maxXp);

        AddStats();
        playerContr.FullHeal();
    }

    private void AddStats()
    {
        // 1:Speed, 2:AttackCooldown, 3:AttackDamage, 4:MaxHP
        int statToIncrease = Random.Range(1, 5); 

        switch (statToIncrease)
        {
            case 1: 
                playerContr.speed *= 1.1f;
                break;
            case 2:
                playerContr.attackCooldown /= 1.1f;
                break;
            case 3:
                playerContr.attackDamage *= 1.1f;
                break;
            case 4:
                playerContr.maxHealth *= 1.1f;
                break;

            default:
                playerContr.maxHealth *= 1.1f;
                break;
        }
    }
}