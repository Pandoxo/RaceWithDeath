
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOffest = 0.05f;
    public ContactFilter2D movementFilter;

    GameObject ambulance;
    Animator animator;
    Vector2 movementInput;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    Transform playerTransform;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    Vector2 lastMove;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        ambulance = GetComponent<GameObject>();
        playerTransform = GetComponent<Transform>();
        ambulance = GameObject.FindGameObjectWithTag("ambulance");


    }

 
    private void FixedUpdate()
    {
        if(movementInput != Vector2.zero)
        {
            bool succes = TryMove(movementInput);
            lastMove = movementInput;
            animator.SetFloat("Horizontal",movementInput.x);
            animator.SetFloat("Vertical",movementInput.y);

            if (!succes){
                succes = TryMove(new Vector2(movementInput.x ,0));
                if (!succes){
                    succes = TryMove(new Vector2(0,movementInput.y));
                }
            }
            animator.SetBool("isMoving",succes);
        }else{
            if (lastMove.x >0){
                animator.Play("PlayerIdle",0,0.99f);
            } else if (lastMove.x < 0){
                animator.Play("PlayerIdle",0,0.0f);
            } else if (lastMove.y > 0){
                animator.Play("PlayerIdle",0,0.5f);
            }else if (lastMove.y < 0){
                animator.Play("PlayerIdle",0,0.25f);
            }

            animator.SetBool("isMoving",false);
        }

        

    }

    private void EnterAmbulance()
    {

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

    void OnEnterVehicle()
    {
        if ((ambulance.transform.position - playerTransform.position).magnitude < 5.0f){
            Debug.Log("Enter");


        }

    }
}
