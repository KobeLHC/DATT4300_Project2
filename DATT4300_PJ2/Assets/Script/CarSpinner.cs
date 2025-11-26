using UnityEngine;

public class CarSpinner : MonoBehaviour
{
    public float spinSpeed = 50f;

    private bool spinning = false;
    private Quaternion defaultRotation;

    void Start()
    {
        defaultRotation = transform.rotation;
    }

    void Update()
    {
        if (spinning)
            transform.Rotate(Vector3.up * spinSpeed * Time.deltaTime, Space.World);
    }

    public void StartSpin()
    {
        spinning = true;
    }

    public void StopSpin()
    {
        spinning = false;

        transform.rotation = defaultRotation;
    }
}