using UnityEngine;
using System.Linq;

public class AutoAttack : MonoBehaviour
{
    [Header("References")]
    public PlayerStats stats;                // ScriptableObject
    public GameObject projectilePrefab;      // Prefab of the projectile

    private float attackTimer = 0f;

    void Update()
    {
        HandleAutoAttack();
    }

    private void HandleAutoAttack()
    {
        attackTimer -= Time.deltaTime;
        if (attackTimer > 0) return;

        // Reset timer
        attackTimer = stats.attackCooldown;

        // Find nearest active enemy
        var target = ObjectPooler.Instance.GetActiveEnemies()
            .OrderBy(e => Vector3.Distance(transform.position, e.transform.position))
            .FirstOrDefault();

        if (target == null) return;

        FireProjectile(target);
    }

    private void FireProjectile(EnemyAI target)
    {
        if (projectilePrefab == null || transform == null) return;

        // Spawn projectile
        GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        // Set its direction toward the target
        Vector3 dir = (target.transform.position - transform.position).normalized;
        proj.transform.forward = dir;

        // Give projectile the damage value
        Bullet p = proj.GetComponent<Bullet>();
        if (p != null)
        {
            p.SetDamage(stats.attackDamage);
            p.SetDirection(dir);
        }
    }
}
