using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public RectTransform joystickBG;  // Background area
    public RectTransform handle;      // The knob
    public float handleRange = 100f;  // Max distance handle can move

    private Vector2 inputVector;

    // Called when pointer/touch starts on joystickBG
    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    // Called while dragging
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            joystickBG, 
            eventData.position, 
            eventData.pressEventCamera, 
            out localPoint
        );

        // normalize relative to joystickBG radius
        Vector2 normalized = localPoint / (joystickBG.sizeDelta / 2f);
        inputVector = Vector2.ClampMagnitude(normalized, 1f);

        // move handle
        handle.anchoredPosition = inputVector * (joystickBG.sizeDelta / 2f);
    }

    // Called when touch ends
    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }

    // Public getter for input (normalized direction)
    public Vector2 InputVector
    {
        get
        {
            if (inputVector.sqrMagnitude > 0.01f)
                return inputVector.normalized; // always returns direction only
            return Vector2.zero;
        }
    }
}
