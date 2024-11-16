using UnityEngine;

public class CarController : MonoBehaviour
{

    [Header("Car settings")]
    public float accelerationFactor = 30.0f;
    public float turnFactor = 3.5f; 

    float accelerationInput = 0;
    float steeringInput = 0;
    float rotationAngle = 0;  

    Rigidbody2D carRb;
    void Awake()
    {
        carRb = GetComponent<Rigidbody2D>();
    } 
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    // Update is called once per frame
    void FixedUpdate()
    {
        ApplyEngineForce();
        AplaySteering();

    }

    void ApplyEngineForce()
    {
        Vector2 engineForceVector = transform.up * accelerationFactor * accelerationInput;
        carRb.AddForce(engineForceVector,ForceMode2D.Force);

    }

    void AplaySteering()
    {
        rotationAngle = steeringInput * turnFactor;
        carRb.MoveRotation(rotationAngle);
        
    }
}
