using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;      // The player’s transform
    public float smoothSpeed = 0.125f;
    public Vector3 offset;        // Optional: offset from the player

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        // Lock the camera's Z position to -10 (or whatever your camera's Z should be)
        desiredPosition.z = -10f;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
