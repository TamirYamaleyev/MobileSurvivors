using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public VirtualJoystick joystick;

    public static event Action<Vector2> OnMove;
    public static event Action OnDash;

    void Update()
    {
        CheckMovement();
        CheckDash();
    }

    void CheckMovement()
    {
        Vector2 move = Vector2.zero;

        // Keyboard input
        move += new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // Joystick input
        if (joystick != null)
            move += new Vector2(joystick.Horizontal(), joystick.Vertical());

        // Ignore tiny input
        if (move.sqrMagnitude < 0.01f)
            move = Vector2.zero;
        else
            move.Normalize();

        OnMove?.Invoke(move);
    }

    void CheckDash()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            OnDash?.Invoke();
    }

    // Call this from the UI dash button
    public void DashButtonPressed()
    {
        OnDash?.Invoke();
    }
}
