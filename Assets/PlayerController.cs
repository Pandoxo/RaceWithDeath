
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOffest = 0.05f;
    public ContactFilter2D movementFilter;

    Vector2 movementInput;
    Rigidbody2D rb;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

 
    private void FixedUpdate()
    {
        if(movementInput != Vector2.zero)
        {
            bool succes = TryMove(movementInput);

            if (!succes){
                succes = TryMove(new Vector2(movementInput.x ,0));
                if (!succes){
                    succes = TryMove(new Vector2(0,movementInput.y));
                }
            }
        }

    }

    private bool TryMove(Vector2 direction)
    {
        int count = rb.Cast(
                movementInput,
                movementFilter,
                castCollisions,
                moveSpeed * Time.fixedDeltaTime + collisionOffest
            );
            if (count == 0){
                rb.MovePosition(rb.position + movementInput * moveSpeed * Time.fixedDeltaTime);
                return true;
            } else {
                return false;
            }

    }
    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();



    }
}
