using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 1f;

    private void OnTriggerEnter(Collider other)
    {
        // Hit enemies only
        if (other.CompareTag("Enemy"))
        {
            // Here you could call enemy.TakeDamage(damage) if you implement it
            // Example: other.GetComponent<EnemyHealth>()?.TakeDamage(damage);

            // Return bullet to the pool
            ObjectPooler.Instance.ReturnToPool(gameObject);
        }

        // Optional: return bullet if it hits walls/obstacles
        if (other.CompareTag("Obstacle"))
        {
            ObjectPooler.Instance.ReturnToPool(gameObject);
        }
    }

    private void OnEnable()
    {
        // Reset Rigidbody velocity so pooling doesn�t carry old speed
        if (TryGetComponent<Rigidbody>(out Rigidbody rb))
            rb.linearVelocity = Vector3.zero;
    }
}
