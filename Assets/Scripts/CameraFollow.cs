using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public bool cameraLocked;
    public Transform player;
    public Vector3 offset;
    public float smoothFactor = 60f;

    // LateUpdate is called after all Update functions have been called
    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 desiredPosition = player.position + offset;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothFactor * Time.deltaTime);
        }
    }
}
