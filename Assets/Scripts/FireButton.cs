using UnityEngine;
using UnityEngine.EventSystems;

public class FireButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool IsHeld { get; private set; }

    private bool pressed;

    public void OnPointerDown(PointerEventData eventData)
    {
        IsHeld = true;
        pressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        IsHeld = false;
    }

    private void OnDisable()
    {
        IsHeld = false;
        pressed = false;
    }

    public bool ConsumePress()
    {
        bool result = pressed;
        pressed = false;
        return result;
    }
}
