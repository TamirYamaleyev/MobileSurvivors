using UnityEngine;

public class TopDownSurvivorsCamera : MonoBehaviour
{
    public Transform target;          // player
    public float smoothSpeed = 10f;   // camera catch-up speed
    public float height = 15f;        // how high above the player
    public Vector3 offset = Vector3.zero;

    void LateUpdate()
    {
        if (!target) return;

        // desired camera position (X/Z follow, Y stays constant)
        Vector3 desiredPos = new Vector3(
            target.position.x + offset.x,
            height,
            target.position.z + offset.z
        );

        // smooth follow
        transform.position = Vector3.Lerp(
            transform.position,
            desiredPos,
            smoothSpeed * Time.deltaTime
        );

        // force top-down angle (look straight down)
        transform.rotation = Quaternion.Euler(90f, 0f, 0f);
    }
}
