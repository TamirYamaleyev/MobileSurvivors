using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public static event Action<Vector2> OnMove;
    public static event Action OnDash;

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

    void CheckMovement()
    {
        Vector2 move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        OnMove?.Invoke(move);
    }

    void CheckDash()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            OnDash?.Invoke();
    }
}
