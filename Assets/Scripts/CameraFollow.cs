using UnityEngine;

public class TopDownSurvivorsCamera : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 10f;

    public float landscapeHeight = 30f;
    public float portraitHeight = 40f;

    public Vector3 offset = Vector3.zero;

    private float targetHeight;
    private ScreenOrientation lastOrientation;

    void Start()
    {
        lastOrientation = Screen.orientation;
        ApplyOrientationSettings();
    }

    void Update()
    {
        if (Screen.orientation != lastOrientation)
        {
            lastOrientation = Screen.orientation;
            ApplyOrientationSettings();
        }
    }

    void ApplyOrientationSettings()
    {
        // 1. Set height (your values)
        targetHeight = (Screen.width > Screen.height)
            ? landscapeHeight
            : portraitHeight;

        // 2. Maintain constant horizontal FOV
        float targetHorizontalFOV = 90f;  // pick what looks best (80–100 usually)

        float aspect = (float)Screen.width / Screen.height;

        // Convert horizontal FOV to vertical FOV
        float verticalFOV = 2f * Mathf.Atan(Mathf.Tan(targetHorizontalFOV * Mathf.Deg2Rad / 2f) / aspect) * Mathf.Rad2Deg;

        Camera.main.fieldOfView = verticalFOV;
    }


    void LateUpdate()
    {
        if (!target) return;

        float currentHeight = Mathf.Lerp(transform.position.y, targetHeight, smoothSpeed * Time.deltaTime);

        Vector3 desiredPos = new Vector3(
            target.position.x + offset.x,
            currentHeight,
            target.position.z + offset.z
        );

        transform.position = desiredPos;

        transform.rotation = Quaternion.Euler(84f, 0f, 0f);
    }
}
