using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public VirtualJoystick joystick;

    public static event Action<Vector2> OnMove;
    public static event Action OnDash;
    public static event Action<int> OnTakeDamage;

    // Update is called once per frame
    void Update()
    {
        CheckMovement();
        CheckDash();
    }

    public void Move(Vector2 direction)
    {
        OnMove?.Invoke(direction);
    }

    public void DashButtonPressed()
    {
        OnDash?.Invoke();
    }

    public void TakeDamage(int amount)
    {
        OnTakeDamage?.Invoke(amount);
    }

    void CheckMovement()
    {
        Vector2 move = Vector2.zero;

        // Keyboard / old Input system
        move.x += Input.GetAxisRaw("Horizontal");
        move.y += Input.GetAxisRaw("Vertical");

        // Joystick input
        if (joystick != null)
        {
            move.x += joystick.Horizontal();
            move.y += joystick.Vertical();
        }

        // Clamp magnitude so diagonal isn’t faster
        move = Vector2.ClampMagnitude(move, 1f);

        // Fire the event
        OnMove?.Invoke(move);
    }



    void CheckDash()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            OnDash?.Invoke();
    }
}
