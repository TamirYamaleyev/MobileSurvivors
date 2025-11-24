using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image fillImage;

    public void UpdateHealthBar(float health, float maxHealth)
    {
        if (fillImage != null)
            fillImage.fillAmount = health / maxHealth;
    }
}
