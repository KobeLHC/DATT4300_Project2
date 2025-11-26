using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeIntensity = 0.05f;
    public float shakeSpeed = 20f;

    private Vector3 originalPos;
    private bool shaking = false;

    void Start()
    {
        originalPos = transform.localPosition;
    }

    void Update()
    {
        if (shaking)
        {
            transform.localPosition = originalPos +
                (Random.insideUnitSphere * shakeIntensity);
        }
        else
        {
            // smoothly return to normal
            transform.localPosition = Vector3.Lerp(
                transform.localPosition,
                originalPos,
                Time.deltaTime * shakeSpeed
            );
        }
    }

    public void StartShake() => shaking = true;
    public void StopShake() => shaking = false;
}