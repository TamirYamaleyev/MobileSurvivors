using UnityEngine;

public class FloatingOrigin : MonoBehaviour
{
    [Header("References")]
    public Transform player;        // Player transform
    public Transform worldRoot;     // Parent of all movable objects (decorations, obstacles, pools)

    [Header("Settings")]
    public float snapDistance = 1000f;  // Distance threshold to snap

    void Update()
    {
        if (player == null || worldRoot == null)
            return;

        // Check if player has moved too far
        if (player.position.magnitude > snapDistance)
        {
            SnapOrigin();
        }
    }

    void SnapOrigin()
    {
        Vector3 offset = player.position;

        // Snap player back to origin
        player.position = Vector3.zero;

        // Move all world objects by the negative offset
        worldRoot.position -= offset;

        // Optional: if using physics objects, you may need to reset Rigidbody interpolation
        // foreach (var rb in worldRoot.GetComponentsInChildren<Rigidbody>())
        //     rb.position -= offset;
    }
}
