using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 3f;         // Movement speed
    public float stopDistance = 1.5f;    // How close to get before stopping
    public float rotationSpeed = 10f;    // How fast enemy rotates to face player
    public int scoreToGive = 10;

    private Transform player;
    private Rigidbody rb;

    void OnEnable()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();

        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
        }

        rb.linearVelocity = Vector3.zero;
    }

    void FixedUpdate()
    {
        if (player == null) return;

        // Offset in XZ plane (ignore Y)
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

        // Rotate only around Y
        if (offset.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(offset, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }

    void OnDisable()
    {
        if (rb != null)
            rb.linearVelocity = Vector3.zero;
    }

    public void Die()
    {

    }
}
