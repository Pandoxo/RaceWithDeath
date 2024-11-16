
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOffest = 0.05f;
    public ContactFilter2D movementFilter;


    public bool driving = false;
    GameObject ambulance;
    GameObject cineMachineCameraObject;
    CinemachineCamera cinemachineCamera;
    Animator animator;
    Vector2 movementInput;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    BoxCollider2D playerCollider;
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
        cineMachineCameraObject = GameObject.Find("CM vcam1");
        cinemachineCamera = cineMachineCameraObject.GetComponent<CinemachineCamera>();
        playerTransform = GetComponent<Transform>();
        playerCollider = GetComponent<BoxCollider2D>();
        ambulance = GameObject.FindGameObjectWithTag("ambulance");


    }

    private void setIdleSprite()
    {
        if (lastMove.x >0){
                animator.Play("PlayerIdle",0,0.99f);
            } else if (lastMove.x < 0){
                animator.Play("PlayerIdle",0,0.0f);
            } else if (lastMove.y > 0){
                animator.Play("PlayerIdle",0,0.5f);
            }else if (lastMove.y < 0){
                animator.Play("PlayerIdle",0,0.25f);
            }
    }
 
    private void FixedUpdate()
    {
        if (driving)
        {
            movementInput = Vector2.zero;
            playerTransform.position = ambulance.transform.position;
        }
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
            setIdleSprite();
            animator.SetBool("isMoving",false);
        }
    }

    private void EnterAmbulance()
    {
        spriteRenderer.enabled = false;
        playerCollider.enabled = false;
        cinemachineCamera.Follow = ambulance.transform;

    }
    private void ExitAmbulance()
    {
        spriteRenderer.enabled = true;
        playerCollider.enabled = true;
        cinemachineCamera.Follow = playerTransform;
        playerTransform.position = ambulance.transform.TransformPoint(new Vector3(-1,0,0));
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
        if (driving)
        {
            ExitAmbulance();
            driving = false;
            return;
        }else
        {
            if ((ambulance.transform.position - playerTransform.position).magnitude < 5.0f)
            {
                driving = true;
                EnterAmbulance();
            }
        }
    }
}
