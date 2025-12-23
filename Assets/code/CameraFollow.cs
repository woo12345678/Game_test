using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;             
    public Vector3 offset = new Vector3(0, 10, -13); 
    public float smoothTime = 0.12f;

    private Vector3 vel;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desired = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, desired, ref vel, smoothTime);

        
    }
}
