using UnityEngine;
using UnityEngine.EventSystems;

public class TouchLookArea : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private float sensitivity = 0.15f;

    private const int NoPointer = -100;

    private int pointerId = NoPointer;
    private Vector2 delta;

    public void OnPointerDown(PointerEventData eventData)
    {
        pointerId = eventData.pointerId;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.pointerId != pointerId)
        {
            return;
        }

        delta += eventData.delta * sensitivity;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerId == pointerId)
        {
            pointerId = NoPointer;
        }
    }

    private void OnDisable()
    {
        pointerId = NoPointer;
        delta = Vector2.zero;
    }

    public Vector2 ConsumeDelta()
    {
        Vector2 result = delta;
        delta = Vector2.zero;
        return result;
    }
}
