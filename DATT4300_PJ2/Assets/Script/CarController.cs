using UnityEngine;
using TMPro;


[RequireComponent(typeof(AudioSource))]
public class CarController : MonoBehaviour
{

    [Header("References")]
    public Transform carModel; // the visible car mesh

    [Header("UI")]
    public TextMeshProUGUI kphText;

    [Header("Car Settings")]
    public float acceleration = 20f;
    public float turnSpeed = 60f;
    public float maxSpeed = 25f;
    public float groundCheckDistance = 1.2f;

    [Header("Audio")]
    public AudioClip engineIdleClip; //  idle sound here
    public AudioClip accelerationClip;  // revving
    public float pitchRange = 1.5f;     // how much pitch changes
    public float volumeRange = 0.5f;    // how much volume changes

    public float idleSpeedThreshold = 0.1f; // speed considered “stopped”

    private Rigidbody rb;
    private float moveInput;
    private float turnInput;
    private bool grounded;
    private AudioSource audioSource;
       private bool isAccelerating;


       [Header("Explosion")]
    public AudioSource explosionAudio; 
        public ParticleSystem explosionParticle; 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -0.5f, 0); // stability

        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true; // loop the engine sound
        audioSource.clip = engineIdleClip;
    }

    void Update()
    {
        moveInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");

        HandleEngineAudio();
        UpdateSpeedometer();

    }

        void UpdateSpeedometer()
    {
        if (kphText == null) return;

        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        float speed = flatVel.magnitude * 3.6f;
       kphText.text = Mathf.RoundToInt(speed).ToString();;
    }

    void FixedUpdate()
    {
        CheckGround();

        if (grounded)
        {
            // forward/backward acceleration
            Vector3 force = transform.forward * moveInput * acceleration;
            rb.AddForce(force, ForceMode.Acceleration);

            // realistic turning
            float speedFactor = rb.velocity.magnitude / maxSpeed; // 0 to 1
            float turn = turnInput * turnSpeed * Time.fixedDeltaTime;

            // invert turn when moving backward
            if (moveInput < 0f) turn = -turn;

            // scale turn by speed for smoothness
            turn *= Mathf.Lerp(0.5f, 1f, speedFactor);

            rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, turn, 0f));

            // limit speed
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            if (flatVel.magnitude > maxSpeed)
                rb.velocity = flatVel.normalized * maxSpeed + Vector3.up * rb.velocity.y;
        }
        else
        {
            // add downforce to help land faster
            rb.AddForce(Vector3.down * 50f, ForceMode.Acceleration);
        }

        // keep car model aligned with movement
        if (carModel)
        {
            Vector3 vel = rb.velocity;
            if (vel.magnitude < 0.1f) vel = transform.forward;
            Quaternion targetRot = Quaternion.LookRotation(vel.normalized, Vector3.up);
            carModel.rotation = Quaternion.Slerp(carModel.rotation, targetRot, 10f * Time.deltaTime);
        }
    }

    void CheckGround()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);
    }

void HandleEngineAudio()
{
    // get flat velocity (ignore vertical)
    Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
    float speed = flatVel.magnitude;

    // normalize speed 0–1 for pitch scaling
    float speedPercent = Mathf.Clamp01(speed / maxSpeed);

    // idle sound when stopped
    if (speed < idleSpeedThreshold)
    {
        if (audioSource.clip != engineIdleClip)
        {
            audioSource.clip = engineIdleClip;
            audioSource.loop = true;
            audioSource.Play();
        }
        audioSource.pitch = 1f; // idle pitch
    }
    // acceleration sound when moving
    else
    {
        if (audioSource.clip != accelerationClip)
        {
            audioSource.clip = accelerationClip;
            audioSource.loop = true;
            audioSource.Play();
        }
        // pitch scales with speed (1.0 at stop → pitchRange at max speed)
        audioSource.pitch = Mathf.Lerp(1f, pitchRange, speedPercent);
    }
}


    void OnCollisionEnter(Collision collision)
{
  if (collision.gameObject.CompareTag("Building"))
{
    if (explosionParticle)
        explosionParticle.Play();

    if (explosionAudio)
        explosionAudio.Play();

           // hide the child called "Car"
        Transform carChild = transform.Find("Car"); // find the child
        if (carChild != null)
            carChild.gameObject.SetActive(false);   // disable it
              // Make the car immobile
            if (rb != null)
            {
                rb.velocity = Vector3.zero;        // stop all motion
                rb.angularVelocity = Vector3.zero; // stop rotation
                rb.isKinematic = true;             // prevent physics from moving it
            }
    }
    


}
}

