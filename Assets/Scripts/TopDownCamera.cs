using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
    public Transform player;
    public float distance = 10f;     // How far behind the player
    public float height = 10f;       // How high above the player
    public float tiltAngle = 30f;    // Camera tilt in degrees

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;    
    }

    void LateUpdate()
    {
        if (player == null) return;

        // Calculate offset based on tilt
        float rad = Mathf.Deg2Rad * tiltAngle;
        Vector3 offset = new Vector3(0, height, -distance);

        // Move camera to player + offset
        transform.position = player.position + offset;

        // Look at the player (tilted)
        transform.rotation = Quaternion.Euler(tiltAngle, 0f, 0f);
    }
}
