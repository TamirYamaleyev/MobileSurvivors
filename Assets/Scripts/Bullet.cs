using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [Header("Projectile Settings")]
    public float speed = 20f;
    public float lifetime = 5f;  // Destroy after this time

    private float damage;
    private Vector3 direction;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true; // Using MovePosition instead of physics velocity
    }

    private void OnEnable()
    {
        // Auto-destroy after lifetime
        Invoke(nameof(DestroyProjectile), lifetime);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void FixedUpdate()
    {
        if (direction != Vector3.zero)
        {
            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
        }
    }

    public void SetDamage(float dmg)
    {
        damage = dmg;
    }

    public void SetDirection(Vector3 dir)
    {
        direction = dir.normalized;
        transform.forward = direction;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Only react to objects on the Enemy layer
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            EnemyAI enemy = other.GetComponent<EnemyAI>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                DestroyProjectile();
            }
        }
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
