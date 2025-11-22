using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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
    public AudioClip engineIdleClip; // idle sound
    public AudioClip accelerationClip;  // revving
    public float pitchRange = 1.5f;     // max pitch
    public float volumeRange = 0.5f;    // max volume

    public float idleSpeedThreshold = 0.1f; // considered stopped

    private Rigidbody rb;
    private float moveInput;
    private float turnInput;
    private bool grounded;
    private AudioSource audioSource;

    [Header("Text")]
    public GameObject loseText;

    [Header("Explosion")]
    public AudioSource explosionAudio;
    public ParticleSystem explosionParticle;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -0.5f, 0);

  // car choice set stats
        int choice = PlayerPrefs.GetInt("CarChoice", 1); // default to car 1 if not set

        switch (choice)
        {
            case 1:
                acceleration = 25f;
                maxSpeed = 30f;
                turnSpeed = 90f;
                break;

            case 2:
                acceleration = 20f;
                maxSpeed = 50f;
                turnSpeed = 75f;
                break;

            case 3:
                acceleration = 20f;
                maxSpeed = 20f;
                turnSpeed = 100f;
                break;

            case 4:
                acceleration = 20f;
                maxSpeed = 40f;
                turnSpeed = 80f;
                break;
        }
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.clip = engineIdleClip;
        audioSource.Play();
        loseText.SetActive(false);
    }

    void Update()
{
    // Forward / backward
    if (Input.GetKey(KeyCode.W))
        moveInput = 1f;
    else if (Input.GetKey(KeyCode.K))
        moveInput = -1f;
    else
        moveInput = 0f;

    // gets current speed
    Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
    float kph = flatVel.magnitude * 3.6f;

    // only turns if above
    if (kph > 1f)
    {
        if (Input.GetKey(KeyCode.A))
            turnInput = -1f;
        else if (Input.GetKey(KeyCode.L))
            turnInput = 1f;
        else
            turnInput = 0f;
    }
    else
    {
        turnInput = 0f; // too slow to steer
    }

    HandleEngineAudio();
    UpdateSpeedometer();
}

    void UpdateSpeedometer()
    {
        if (kphText == null) return;

        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        float speed = flatVel.magnitude * 3.6f;
        kphText.text = Mathf.RoundToInt(speed).ToString();
    }

    void FixedUpdate()
    {
        CheckGround();

        if (grounded)
        {
            // Acceleration
            Vector3 force = transform.forward * moveInput * acceleration;
            rb.AddForce(force, ForceMode.Acceleration);

            // Turning
            float speedFactor = rb.velocity.magnitude / maxSpeed;
            float turn = turnInput * turnSpeed * Time.fixedDeltaTime;

            if (moveInput < 0f) turn = -turn; // invert turn when going backward
            turn *= Mathf.Lerp(0.5f, 1f, speedFactor);

            rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, turn, 0f));

            // Limit max speed
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            if (flatVel.magnitude > maxSpeed)
                rb.velocity = flatVel.normalized * maxSpeed + Vector3.up * rb.velocity.y;
        }
        else
        {
            // Downforce
            rb.AddForce(Vector3.down * 50f, ForceMode.Acceleration);
        }

        // Align car model with movement
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
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        float speed = flatVel.magnitude;
        float speedPercent = Mathf.Clamp01(speed / maxSpeed);

        // Idle sound
        if (speed < idleSpeedThreshold)
        {
            if (audioSource.clip != engineIdleClip)
            {
                audioSource.clip = engineIdleClip;
                audioSource.loop = true;
                audioSource.Play();
            }
            audioSource.pitch = 1f;
        }
        else // Acceleration sound
        {
            if (audioSource.clip != accelerationClip)
            {
                audioSource.clip = accelerationClip;
                audioSource.loop = true;
                audioSource.Play();
            }
            audioSource.pitch = Mathf.Lerp(1f, pitchRange, speedPercent);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Building"))
        {
            StartCoroutine(crashCountDown());

            if (explosionParticle) explosionParticle.Play();
            if (explosionAudio) explosionAudio.Play();
            loseText.SetActive(true);

            // Hide car mesh
            Transform carChild = transform.Find("Car");
            if (carChild != null) carChild.gameObject.SetActive(false);

            // Make car immobile
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.isKinematic = true;
            }
        }
    }

    IEnumerator crashCountDown()
    {
        yield return new WaitForSeconds(1.5f);
        loseText.SetActive(false);
        SceneManager.LoadScene("Start");
    }
}
