using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private InputHandler input;
    public PlayerStats stats;

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
}
