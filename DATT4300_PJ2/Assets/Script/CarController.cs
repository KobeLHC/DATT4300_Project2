using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float maxMotorTorque = 500f;  // torque applied to wheels
    public float maxSteerAngle = 30f;    // front wheel steering
    public float maxSpeed = 20f;

    [Header("Wheels")]
    public WheelCollider frontLeftWheel;
    public WheelCollider frontRightWheel;
    public WheelCollider rearLeftWheel;
    public WheelCollider rearRightWheel;
    public Transform[] wheelMeshes;      // optional: visual meshes

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -0.5f, 0); // lowers center of mass for stability
    }

    void FixedUpdate()
    {
        float moveInput = Input.GetAxis("Vertical");
        float steerInput = Input.GetAxis("Horizontal");

        // motor torque to rear wheels
        rearLeftWheel.motorTorque = moveInput * maxMotorTorque;
        rearRightWheel.motorTorque = moveInput * maxMotorTorque;

        // sterring front wheels
        frontLeftWheel.steerAngle = steerInput * maxSteerAngle;
        frontRightWheel.steerAngle = steerInput * maxSteerAngle;

        // max speed limit
        Vector3 horizontalVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        if (horizontalVel.magnitude > maxSpeed)
        {
            horizontalVel = horizontalVel.normalized * maxSpeed;
            rb.velocity = new Vector3(horizontalVel.x, rb.velocity.y, horizontalVel.z);
        }

        // visuals
        if (wheelMeshes != null && wheelMeshes.Length == 4)
        {
            WheelCollider[] wheels = { frontLeftWheel, frontRightWheel, rearLeftWheel, rearRightWheel };
            for (int i = 0; i < 4; i++)
            {
                wheels[i].GetWorldPose(out Vector3 pos, out Quaternion rot);
                wheelMeshes[i].position = pos;
                wheelMeshes[i].rotation = rot;
            }
        }
    }
}
