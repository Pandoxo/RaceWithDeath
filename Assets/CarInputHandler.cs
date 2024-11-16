using UnityEngine;
using UnityEngine.InputSystem;
public class CarInputHandler : MonoBehaviour
{
    
    CarController carController;
    GameObject player;
    PlayerController playerController;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        carController = GetComponent<CarController>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
  

    void OnMove(InputValue movementValue){
        if (playerController.driving)
        {
        carController.SetInputVector(movementValue.Get<Vector2>());
        Debug.Log(movementValue.Get<Vector2>());
        }
    }
}
