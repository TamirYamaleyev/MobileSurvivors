using UnityEngine;
using UnityEngine.UI;

public class GettingXp : MonoBehaviour
{
    public Image fillImage;

    public void UpdateXpBar(float xp, float maxXp)
    {
        if (fillImage != null)
            fillImage.fillAmount = xp / maxXp;
    }
}