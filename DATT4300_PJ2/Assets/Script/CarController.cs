using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("References")]
    public Transform carModel; // the visible car mesh

    [Header("Car Settings")]
    public float acceleration = 20f;
    public float turnSpeed = 60f;
    public float maxSpeed = 25f;
    public float groundCheckDistance = 1.2f;

    private Rigidbody rb;
    private float moveInput;
    private float turnInput;
    private bool grounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -0.5f, 0); // stability
    }

    void Update()
    {
        moveInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
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
}
