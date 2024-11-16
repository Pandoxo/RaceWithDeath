using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{

    [Header("Car settings")]
    public float driftFactor = 0.95f;
    public float accelerationFactor = 30.0f;
    public float turnFactor = 3.5f; 


    float accelerationInput = 0;
    float steeringInput = 0;
    float rotationAngle;
    
    Transform transform;
    Rigidbody2D carRb;
    GameObject player;

    void Awake()
    {
        carRb = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();
        rotationAngle =  transform.rotation.z;
        
        
    } 
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    // Update is called once per frame
    void FixedUpdate()
    {
        ApplyEngineForce();
        KillOrthogonalVelocity();
        AplaySteering();

    }

    void ApplyEngineForce()
    {
        if (accelerationInput ==0)
        {
            carRb.linearDamping = Mathf.Lerp(carRb.linearDamping,3.0f,Time.fixedDeltaTime *3);
        }else
        {
            carRb.linearDamping = 0;
        }

        Vector2 engineForceVector = transform.up * accelerationFactor * accelerationInput;
        carRb.AddForce(engineForceVector,ForceMode2D.Force);

    }

    void KillOrthogonalVelocity()
    {
        Vector2 forwardVelocity = transform.up *Vector2.Dot(carRb.linearVelocity,transform.up);
        Vector2 rightVelocity = transform.right *Vector2.Dot(carRb.linearVelocity,transform.right);

        carRb.linearVelocity = forwardVelocity + rightVelocity * driftFactor;

    }

    void AplaySteering()
    {
        float minSpeedBeforeAllowTurningFactor = carRb.linearVelocity.magnitude /8;
        minSpeedBeforeAllowTurningFactor = Mathf.Clamp01(minSpeedBeforeAllowTurningFactor);
        rotationAngle -= steeringInput * turnFactor * minSpeedBeforeAllowTurningFactor;
        carRb.MoveRotation(rotationAngle);
        
    }

    public void SetInputVector(Vector2 inputVector)
    {   
        accelerationInput = inputVector.y;
        steeringInput = inputVector.x;
   
    }

}
