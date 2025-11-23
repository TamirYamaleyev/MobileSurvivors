using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public VirtualJoystick joystick;

    private PlayerControls controls;
    public bool DashPressed { get; set; } = false;
    public Vector2 MoveInput { get; private set; }

    void Awake()
    {
        controls = new PlayerControls();
        controls.Player.Dash.performed += ctx => DashPressed = true;
    }

    void OnEnable() => controls.Enable();
    void OnDisable() => controls.Disable();

    void Update()
    {
        Vector2 move = controls.Player.Move.ReadValue<Vector2>();

        if (joystick != null && joystick.InputVector.sqrMagnitude > 0.01f)
            move = joystick.InputVector;

        MoveInput = move;
    }

    public void DashButtonPressed()
    {
        DashPressed = true;
    }
}
