using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private InputHandler input;
    public PlayerStats stats;

    private float currentHealth;
    private float invincibilityTimer = 0f;
    private int score;

    private Rigidbody rb;

    private Vector2 moveDir;
    private bool isDashing;
    private float dashTime;
    private float dashCooldown;
    private Vector3 dashDirection;

    void Start()
    {
        input = GetComponent<InputHandler>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        currentHealth = stats.maxHealth;
    }

    void Update()
    {
        moveDir = input.MoveInput;

        // Dash cooldown
        if (dashCooldown > 0)
            dashCooldown -= Time.deltaTime;

        // Dash timer
        if (isDashing)
        {
            dashTime -= Time.deltaTime;
            if (dashTime <= 0)
                isDashing = false;
        }

        // Handle dash input
        if (input.DashPressed)
        {
            input.DashPressed = false;
            TryDash();
        }

        // Invincibility countdown
        if (invincibilityTimer > 0)
            invincibilityTimer -= Time.deltaTime;
    }

    private void TryDash()
    {
        if (isDashing || dashCooldown > 0)
            return;

        // Use facing direction
        dashDirection = transform.forward;

        if (dashDirection.sqrMagnitude < 0.01f)
            return;

        isDashing = true;
        dashTime = stats.dashDuration;
        dashCooldown = stats.dashCooldown;
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            rb.linearVelocity = dashDirection * stats.dashSpeed;
            return;
        }

        Vector3 movement = new Vector3(moveDir.x, 0, moveDir.y) * stats.speed;
        rb.linearVelocity = movement;

        if (moveDir.sqrMagnitude > 0.01f)
            transform.forward = new Vector3(moveDir.x, 0, moveDir.y);
    }

    public void AddScore(int amount)
    {
        score += amount;
    }

    public void TakeDamage(float amount)
    {
        // iFrame check
        if (invincibilityTimer > 0)
            return;

        currentHealth -= amount;
        Debug.Log(currentHealth);

        // Update Healthbar

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            // SFX
            // iFrame
        }

        invincibilityTimer = stats.invincibilityDuration;
    }

    private void Die()
    {
        // Restart Game
        Time.timeScale = 0f;
    }

    public void LoadHealth(float savedHealth)
    {
        currentHealth = savedHealth;
    }
    public void LoadScore(int savedScore)
    {
        score = savedScore;
    }

    // ---------------- Getters ----------------
    public float DashCooldown => dashCooldown;
    public float CurrentHealth => currentHealth;
    public int Score => score;
}
