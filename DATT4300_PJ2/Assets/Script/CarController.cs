using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 10f;      // forward backward speed
    public float turnSpeed = 100f;     // rotation speed
    public float maxSpeed = 20f;       // maximum speed
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // optional: lower center of mass so cube doesn't tip easily
        rb.centerOfMass = new Vector3(0, -0.5f, 0);
    }

    void FixedUpdate()
    {
        // Get input
        float moveInput = Input.GetAxis("Vertical");   // inputs for w and s
        float turnInput = Input.GetAxis("Horizontal"); // inputs for a and d

        // forward backward movement
        Vector3 forwardMovement = transform.forward * moveInput * moveSpeed;
        rb.AddForce(forwardMovement, ForceMode.Acceleration);

        // speed limiter
        Vector3 horizontalVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        if (horizontalVelocity.magnitude > maxSpeed)
        {
            horizontalVelocity = horizontalVelocity.normalized * maxSpeed;
            rb.velocity = new Vector3(horizontalVelocity.x, rb.velocity.y, horizontalVelocity.z);
        }

        // rotation
        float turn = turnInput * turnSpeed * Time.fixedDeltaTime;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0, turn, 0));
    }
}
