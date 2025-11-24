using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyAI : MonoBehaviour
{
    [Header("Audio")]
    public AudioClip attackSFX;
    public AudioClip hurtSFX;
    public AudioClip deathSFX;
    private AudioSource audioSource;

    [Header("Stats")]
    public float maxHealth = 100;
    private float currentHealth;
    public int attack = 3; // damage
    public float attackRadius = 1.5f; // how close to hit the player
    public float attackCooldown = 1f; // time between attacks

    [Header("Movement")]
    public float moveSpeed = 3f;
    public float stopDistance = 1.5f;
    public float rotationSpeed = 10f;

    public int scoreToGive = 10;

    private Transform player;
    private Rigidbody rb;

    private float attackTimer = 0f;
    public LayerMask playerLayer; // assign "Player" layer

    void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        currentHealth = maxHealth;

        if (rb == null)
            rb = GetComponent<Rigidbody>();

        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
        }

        rb.linearVelocity = Vector3.zero;
        attackTimer = 0f;
    }

    void FixedUpdate()
    {
        if (player == null) return;

        // Move towards player
        Vector3 offset = player.position - transform.position;
        offset.y = 0;
        float distance = offset.magnitude;

        if (distance > stopDistance)
        {
            Vector3 direction = offset.normalized;
            rb.linearVelocity = direction * moveSpeed;
        }
        else
        {
            rb.linearVelocity = Vector3.zero;
        }

        // Rotate to face player
        if (offset.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(offset, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }

        // Handle attack cooldown
        if (attackTimer > 0)
            attackTimer -= Time.fixedDeltaTime;

        // Try attacking player
        TryAttackPlayer();
    }

    private void TryAttackPlayer()
    {
        if (attackTimer > 0) return;

        // Detect player in range
        Collider[] hits = Physics.OverlapSphere(transform.position, attackRadius, playerLayer);
        foreach (Collider hit in hits)
        {
            PlayerController playerCtrl = hit.GetComponent<PlayerController>();
            if (playerCtrl != null)
            {
                Vector3 hitDir = (playerCtrl.transform.position - transform.position).normalized;
                playerCtrl.TakeDamage(attack);

                // Play attack sound
                audioSource.pitch = Random.Range(0.95f, 1.05f);

                if (attackSFX != null && audioSource != null)
                    audioSource.PlayOneShot(attackSFX);

                // Start cooldown
                attackTimer = attackCooldown;
                break;
            }

        }
    }

    void OnDisable()
    {
        if (rb != null)
            rb.linearVelocity = Vector3.zero;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0) Die();

        else
        {
            audioSource.pitch = Random.Range(0.95f, 1.05f);

            if (hurtSFX != null && audioSource != null)
                audioSource.PlayOneShot(hurtSFX);
        }
    }

    public void Die()
    {
        audioSource.pitch = Random.Range(0.95f, 1.05f);

        if (deathSFX != null && audioSource != null)
            audioSource.PlayOneShot(deathSFX);

        // Give Score

        ObjectPooler.Instance.ReturnToPool(gameObject);
    }

    // Optional: visualize attack radius in editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

    public float CurrentHealth
    {
        get => currentHealth;
        set => currentHealth = value;
    }
}
