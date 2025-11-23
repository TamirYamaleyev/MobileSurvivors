using UnityEngine;

public class CameraOrientationAdjust : MonoBehaviour
{
    public float portraitFOV = 50f;
    public float landscapeFOV = 70f;

    private ScreenOrientation lastOrientation;

    void Start()
    {
        lastOrientation = Screen.orientation;
        ApplySettings();
    }

    void Update()
    {
        if (Screen.orientation != lastOrientation)
        {
            lastOrientation = Screen.orientation;
            ApplySettings();
        }
    }

    void ApplySettings()
    {
        Camera cam = Camera.main;

        if (Screen.width > Screen.height) // Landscape
        {
            cam.fieldOfView = landscapeFOV;
        }
        else // Portrait
        {
            cam.fieldOfView = portraitFOV;
        }
    }
}
