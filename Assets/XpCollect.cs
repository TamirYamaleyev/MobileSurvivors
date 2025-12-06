using UnityEngine;

public class ShardXP : MonoBehaviour
{
    [Header("XP Settings")]
    public int xpAmount = 25;     // how much XP this gem gives

    [Header("Pickup Settings")]
    public bool destroyOnPickup = true;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the thing touching it is the player
        if (other.CompareTag("Player"))
        {
            // Get the player's XP script
            PlayerXP xp = FindObjectOfType<PlayerXP>();

            if (xp != null)
            {
                xp.AddXP(xpAmount);
            }

            if (destroyOnPickup)
            {
                Destroy(gameObject);
            }
        }
    }
}