using UnityEngine;

public class TireSmokeController : MonoBehaviour
{
    public ParticleSystem tireSmoke;
    public float speedThreshold = 20f;
    public float driftAngleThreshold = 18f;

    [Header("Drift Sound")]
    public AudioSource driftAudio;  
    public AudioClip driftClip;
    public float volume = 1f;

    private Rigidbody rb;
    private ParticleSystem.EmissionModule emission;
    private bool isDrifting = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        emission = tireSmoke.emission;
        emission.rateOverTime = 0f;
        

        if (driftAudio != null)
        {
            driftAudio.loop = false;     
            driftAudio.playOnAwake = false;
        }
    }

    void Update()
    {
        float kph = rb.velocity.magnitude * 3.6f;
        float angle = Vector3.Angle(transform.forward, rb.velocity);

        bool driftingNow = (kph >= speedThreshold && angle >= driftAngleThreshold);

     
        if (driftingNow && !isDrifting)
        {
            emission.rateOverTime = 40f;

            if (driftAudio && driftClip)
                driftAudio.PlayOneShot(driftClip, volume);
        }

        else if (!driftingNow && isDrifting)
        {
            emission.rateOverTime = 0f;

            if (driftAudio && driftAudio.isPlaying)
                driftAudio.Stop(); 
        }

        isDrifting = driftingNow;
    }
}
