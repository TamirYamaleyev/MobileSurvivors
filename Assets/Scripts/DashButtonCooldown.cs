using UnityEngine;
using UnityEngine.UI;

public class DashButtonCooldown : MonoBehaviour
{
    public PlayerController player;       // Assign your player
    public Image cooldownOverlay;         // The radial overlay image

    void Update()
    {
        if (player == null || cooldownOverlay == null) return;

        float fill = Mathf.Clamp01(player.DashCooldown / player.stats.dashCooldown);
        cooldownOverlay.fillAmount = fill;
    }
}
