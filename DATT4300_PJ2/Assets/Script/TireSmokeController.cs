using UnityEngine;

public class TireSmokeController : MonoBehaviour
{
    public ParticleSystem tireSmoke;
    public float speedThreshold = 30f;      // must be moving fast
    public float driftAngleThreshold = 35f; // how sideways before smoke

    private Rigidbody rb;
    private ParticleSystem.EmissionModule emission;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        emission = tireSmoke.emission;
        emission.rateOverTime = 0f; // start off
    }

    void Update()
    {
        // get speed in kph
        float kph = rb.velocity.magnitude * 3.6f;

        // get drift angle
        float angle = Vector3.Angle(transform.forward, rb.velocity);

        // drifting = moving fast + sliding sideways
        if (kph >= speedThreshold && angle >= driftAngleThreshold)
        {
            emission.rateOverTime = 40f; // smoke ON
        }
        else
        {
            emission.rateOverTime = 0f; // smoke OFF
        }
    }
}