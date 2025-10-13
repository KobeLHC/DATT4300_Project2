using UnityEngine;

public class CarCamera : MonoBehaviour
{
    public Transform target;              // the car
    public Vector3 offset = new Vector3(0, 3, -8); // height & distance behind
    public float followSpeed = 5f; // follow speed
    public float rotationSpeed = 5f; // rotation speed
    public float forwardTilt = 10f;      // pitch forward

    void LateUpdate()
    {
        if (target == null) return;

        // Smooth follow
        Vector3 desiredPos = target.position + target.TransformDirection(offset);
        transform.position = Vector3.Lerp(transform.position, desiredPos, followSpeed * Time.deltaTime);

        // Smooth rotation
        Vector3 lookDir = target.forward;
        Quaternion desiredRot = Quaternion.LookRotation(lookDir);

        // Apply pitch forward
        desiredRot *= Quaternion.Euler(forwardTilt, 0, 0);

        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRot, rotationSpeed * Time.deltaTime);
    }
}
