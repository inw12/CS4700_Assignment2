using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float smoothing;
    public Vector2 maxPosition;
    public Vector2 minPosition;
    public MinMaxVectorValue camBounds;
    public Vector3Value initialPosition;

    void Start()
    {
        maxPosition = camBounds.max;
        minPosition = camBounds.min;
        transform.position = initialPosition.value;
        //transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }

    // LateUpdate 
    void LateUpdate()
    {
        if (transform.position != target.position)  // "if transform position IS NOT the target position..."
        {   
            // Determine position of the target (the player)
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

            // Return coordinates for where the camera should be within its bounds
            targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);     // Clamp(what we want to bound, min bounds, max bounds); returns value BETWEEN min/max bounds
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);
            
            // Move the camera 
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);   // Lerp(where we are, where we wanna be, movement interval between those 2 points)
        }
    }
}
