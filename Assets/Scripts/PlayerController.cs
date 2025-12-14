using System.Threading.Tasks;
using Unity.Services.Analytics;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private InputHandler input;
    public PlayerStats stats;
    public ScoreHUD scoreUI;

    public float maxHealth;
    public float speed;
    public float attackDamage;
    public float attackCooldown;

    private float currentHealth;
    private float invincibilityTimer = 0f;
    private int score;

    private Rigidbody rb;

    private Vector2 moveDir;
    private bool isDashing;
    private float dashTime;
    private float dashCooldown;
    private Vector3 dashDirection;

    public HealthBar healthBar;

    async void Start()
    {
        maxHealth = stats.maxHealth;
        speed = stats.speed;
        attackDamage = stats.attackDamage;
        attackCooldown = stats.attackCooldown;

        input = GetComponent<InputHandler>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        currentHealth = maxHealth;


        // ----Analytics----
        while (AnalyticsManager.Instance == null)
            await Task.Yield();

        AnalyticsManager.Instance.TrackSessionStart();
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

        Vector3 movement = new Vector3(moveDir.x, 0, moveDir.y) * speed;
        rb.linearVelocity = movement;

        if (moveDir.sqrMagnitude > 0.01f)
            transform.forward = new Vector3(moveDir.x, 0, moveDir.y);
    }

    public void AddScore(int amount)
    {
        score += amount;
        scoreUI.UpdateScoreHUD(score);

        if (score >= 1000)
        {
            AnalyticsService.Instance.RecordEvent("milestone_score_1000");
        }
    }

    public void TakeDamage(float amount)
    {
        // iFrame check
        if (invincibilityTimer > 0)
            return;

        currentHealth -= amount;
        currentHealth = Mathf.Max(currentHealth, 0);

        if (healthBar != null)
            healthBar.UpdateHealthBar(currentHealth, maxHealth);

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
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
    }

    public void LoadScore(int savedScore)
    {
        score = savedScore;
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }

    public void FullHeal()
    {
        currentHealth = maxHealth;
    }

    

    // ---------------- Getters ----------------
    public float DashCooldown => dashCooldown;
    public float CurrentHealth => currentHealth;
    public int Score => score;

   
}
