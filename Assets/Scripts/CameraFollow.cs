using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public bool cameraLocked;
    public Transform target;
    public Vector3 offset;
    public float smoothFactor = 0.5f;

    // LateUpdate is called after all Update functions have been called
    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothFactor * Time.deltaTime);
        }
    }
}
