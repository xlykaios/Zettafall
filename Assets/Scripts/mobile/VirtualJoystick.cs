using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public RectTransform joystickHandleArea;
    private Vector2 inputVector = Vector2.zero;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;
        joystickHandleArea.anchoredPosition = Vector2.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 newInputDirection = Vector2.zero;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            joystickHandleArea.parent as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out newInputDirection
        );

        newInputDirection.x /= (joystickHandleArea.parent as RectTransform).sizeDelta.x;
        newInputDirection.y /= (joystickHandleArea.parent as RectTransform).sizeDelta.y;

        float x = newInputDirection.x * 2 - 1;
        float y = newInputDirection.y * 2 - 1;

        inputVector = new Vector2(x, y);
        inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

        joystickHandleArea.anchoredPosition = new Vector2(
            inputVector.x * ((joystickHandleArea.parent as RectTransform).sizeDelta.x / 3),
            inputVector.y * ((joystickHandleArea.parent as RectTransform).sizeDelta.y) / 3
        );
    }

    public Vector2 GetInputDirection() 
    {
        return new Vector2(inputVector.x, inputVector.y);
    }
}
