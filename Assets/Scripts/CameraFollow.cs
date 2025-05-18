using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothSpeed = 5f;
    [SerializeField] private float yOffset = 2f;

    private float highestY;

    private void LateUpdate()
    {
        if (target == null) return;

        float targetY = target.position.y + yOffset;

        // Only follows Up
        if (targetY > highestY)
        {
            highestY = targetY;
            Vector3 newPos = new Vector3(transform.position.x, highestY, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, newPos, smoothSpeed * Time.deltaTime);
        }
    }
}
