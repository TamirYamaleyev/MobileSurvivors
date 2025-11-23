using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Stats")]
    public PlayerStats stats;

    [Header("Attack")]
    public GameObject projectilePrefab;
    public Transform shootPoint;

    private Vector2 moveDirection;
    private Vector2 lastMoveDirection;

    private bool isDashing = false;
    private float dashTimeRemaining = 0f;
    private float dashCooldownTimer = 0f;
    private Vector3 dashDirection;

    private float attackCooldown;
    private float currentHealth;
    private Rigidbody rb;

    void Start()
    {
        currentHealth = stats.maxHealth;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void OnEnable()
    {
        InputHandler.OnMove += HandleMove;
        InputHandler.OnDash += HandleDash; // Listen for dash button press
    }

    void OnDisable()
    {
        InputHandler.OnMove -= HandleMove;
        InputHandler.OnDash -= HandleDash;
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            rb.linearVelocity = dashDirection * stats.dashSpeed;
        }
        else
        {
            Vector3 movement = new Vector3(moveDirection.x, 0, moveDirection.y) * stats.speed;
            rb.linearVelocity = movement;

            if (moveDirection != Vector2.zero)
                transform.forward = new Vector3(moveDirection.x, 0, moveDirection.y);
        }
    }

    void Update()
    {
        // Dash timer countdown
        if (isDashing)
        {
            dashTimeRemaining -= Time.deltaTime;
            if (dashTimeRemaining <= 0f)
                isDashing = false;
        }

        // Dash cooldown countdown
        if (dashCooldownTimer > 0f)
            dashCooldownTimer -= Time.deltaTime;

        // Shooting
        attackCooldown -= Time.deltaTime;
        if (attackCooldown <= 0f)
        {
            Shoot();
            attackCooldown = stats.attackRate;
        }
    }

    void HandleMove(Vector2 dir)
    {
        moveDirection = dir;

        if (moveDirection.sqrMagnitude > 0.001f)
            lastMoveDirection = moveDirection;
    }

    void HandleDash()
    {
        // Don't dash if cooldown active or already dashing
        if (dashCooldownTimer > 0f || isDashing)
            return;

        // Dash in the current facing direction
        Vector3 dashDir = transform.forward;
        if (dashDir.sqrMagnitude < 0.001f)
            return; // Don't dash if no facing direction

        isDashing = true;
        dashTimeRemaining = stats.dashDuration;
        dashCooldownTimer = stats.dashCooldown;
        dashDirection = dashDir.normalized;
    }

    void Shoot()
    {
        if (projectilePrefab && shootPoint)
        {
            // Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
            // Optional: add projectile damage logic here if needed
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        gameObject.SetActive(false);
    }
}
