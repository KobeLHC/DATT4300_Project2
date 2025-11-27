using UnityEngine;

public class CarCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 3, -8);
    public float followSpeed = 5f;
    public float rotationSpeed = 5f;
    public float forwardTilt = 10f;

    [Header("Speed Shake")]
    public float shakeStartKph = 40f;
    public float maxShakeKph = 80f;
    public float maxShakeIntensity = 0.08f;

    [Header("Burnout Shake")]
public float burnoutShakeIntensity = 0.12f;
public float burnoutShakeTime = 0.3f;

private float burnoutShakeTimer = 0f;

    void LateUpdate()
    {
        if (target == null) return;

        Rigidbody rb = target.GetComponent<Rigidbody>();
        float kph = rb ? rb.velocity.magnitude * 3.6f : 0f;

        Vector3 desiredPos = target.position + target.TransformDirection(offset);

        if (kph >= shakeStartKph)
        {
            float t = Mathf.InverseLerp(shakeStartKph, maxShakeKph, kph);
            float intensity = Mathf.Lerp(0f, maxShakeIntensity, t);

            desiredPos += Random.insideUnitSphere * intensity;
        }

          // burnout shake
        if (burnoutShakeTimer > 0f)
        {
            desiredPos += Random.insideUnitSphere * burnoutShakeIntensity;
            burnoutShakeTimer -= Time.deltaTime;
        }

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPos,
            followSpeed * Time.deltaTime
        );

        Quaternion desiredRot = Quaternion.LookRotation(target.forward);
        desiredRot *= Quaternion.Euler(forwardTilt, 0, 0);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            desiredRot,
            rotationSpeed * Time.deltaTime
        );
    }
    public void TriggerBurnoutShake()
{
    burnoutShakeTimer = burnoutShakeTime;
}

}
