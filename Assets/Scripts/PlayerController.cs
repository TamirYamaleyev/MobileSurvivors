using System;
using System.Xml;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Stats")]
    public PlayerStats stats;

    [Header("Attack")]
    public GameObject projectilePrefab;
    public Transform shootPoint;

    private Vector2 moveDirection;
    private float attackCooldown;
    private float dashCooldownTimer;
    private bool isDashing = false;
    private float dashTime;
    private float currentHealth;

    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = stats.maxHealth;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void OnEnable()
    {
        InputHandler.OnMove += HandleMove;
        InputHandler.OnDash += HandleDash;
    }

    void OnDisable()
    {
        InputHandler.OnMove -= HandleMove;
        InputHandler.OnDash -= HandleDash;
    }

    void FixedUpdate()
    {
        float currentSpeed = isDashing ? stats.dashSpeed : stats.speed;

        if (moveDirection == Vector2.zero)
            rb.linearVelocity = Vector3.zero;
        else
            rb.linearVelocity = new Vector3(moveDirection.x, 0, moveDirection.y) * currentSpeed;

        if (moveDirection != Vector2.zero)
            transform.forward = new Vector3(moveDirection.x, 0, moveDirection.y);
    }

    // Update is called once per frame
    void Update()
    {
        // Automatic shooting
        attackCooldown -= Time.deltaTime;
        if (attackCooldown <= 0f)
        {
            Shoot();
            attackCooldown = stats.attackRate;
        }

        // Dash timer
        if (isDashing)
        {
            dashTime -= Time.deltaTime;
            if (dashTime <= 0f)
                isDashing = false;
        }

        dashCooldownTimer -= Time.deltaTime;
    }

    public Vector3 GetFacingDirection()
    {
        return transform.forward; // or whatever your facing logic is
    }

    void HandleMove(Vector2 dir)
    {
        moveDirection = dir.magnitude > 0.01f ? dir : Vector2.zero;
    }

    void HandleDash()
    {
        if (dashCooldownTimer <= 0f && moveDirection != Vector2.zero)
        {
            isDashing = true;
            dashTime = stats.dashDuration;
            dashCooldownTimer = stats.dashCooldown;
        }
    }

    void Shoot()
    {
        if (projectilePrefab)
        {
            //GameObject proj = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);

            //var projScript = proj.GetComponent<Projectile>();
            //if (projScript != null)
            //    projScript.damage = stats.damage;
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
            Die();
    }

    void Die() => gameObject.SetActive(false);
}
