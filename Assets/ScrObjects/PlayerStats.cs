using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Scriptable Objects/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    public float speed = 5f;
    public float dashSpeed = 15f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    public float attackCooldown = 0.5f;
    public float attackDamage = 1f;
    public float projectileSpeed = 10f;
    public float maxHealth = 100f;
    public float invincibilityDuration = 1f;
}
