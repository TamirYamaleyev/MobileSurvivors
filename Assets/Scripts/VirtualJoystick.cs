using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour
{
    public RectTransform handle;
    public float handleRange = 50f;

    private Vector2 inputVector = Vector2.zero;
    private Vector2 startPos;

    void Start()
    {
        startPos = handle.anchoredPosition;
    }

    void Update()
    {
        // Only works with touch or mouse
        if (Input.GetMouseButton(0))
        {
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                handle.parent as RectTransform,
                Input.mousePosition, null, out pos
            );
            inputVector = Vector2.ClampMagnitude(pos / handleRange, 1f);
            handle.anchoredPosition = inputVector * handleRange;
        }
        else
        {
            inputVector = Vector2.zero;
            handle.anchoredPosition = startPos;
        }
    }

    public float Horizontal() => inputVector.x;
    public float Vertical() => inputVector.y;
}
