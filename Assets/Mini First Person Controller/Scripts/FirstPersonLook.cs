using UnityEngine;

public class FirstPersonLook : MonoBehaviour
{
    [SerializeField] private Transform character;
    [SerializeField] private TouchLookArea lookArea;
    public float sensitivity = 1f;
    public float smoothing = 1.5f;

    private Vector2 velocity;
    private Vector2 frameVelocity;

    private void Reset()
    {
        character = GetComponentInParent<FirstPersonMovement>().transform;
    }

    private void Update()
    {
        Vector2 delta = lookArea != null ? lookArea.ConsumeDelta() : Vector2.zero;
        Vector2 rawFrameVelocity = delta * sensitivity;
        frameVelocity = Vector2.Lerp(frameVelocity, rawFrameVelocity, 1 / smoothing);
        velocity += frameVelocity;
        velocity.y = Mathf.Clamp(velocity.y, -90, 90);

        transform.localRotation = Quaternion.AngleAxis(-velocity.y, Vector3.right);
        character.localRotation = Quaternion.AngleAxis(velocity.x, Vector3.up);
    }

    public Vector2 GetLookAngles()
    {
        return velocity;
    }

    public void SetLookAngles(Vector2 angles)
    {
        velocity = angles;
        frameVelocity = Vector2.zero;
    }
}
