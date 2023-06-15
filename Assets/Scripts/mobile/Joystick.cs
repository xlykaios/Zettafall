using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private Vector2 inputVector;
    private Vector2 originalPosition;
    private Vector2 originalSize;

    [SerializeField] private RectTransform stick;

    private void Start()
    {
        originalPosition = stick.anchoredPosition;
        originalSize = stick.sizeDelta;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 direction = eventData.position - originalPosition;
        inputVector = (direction.magnitude > originalSize.x / 2f) ? direction.normalized : direction / (originalSize.x / 2f);
        stick.anchoredPosition = inputVector * (originalSize.x / 2f);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;
        stick.anchoredPosition = Vector2.zero;
    }

    public Vector2 InputVector
    {
        get { return inputVector; }
    }
}

