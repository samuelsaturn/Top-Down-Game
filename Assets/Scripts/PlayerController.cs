using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float runSpeed = 2f;

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private bool isRunning;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;

    private void Awake()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
        playerControls.Movement.Run.started += OnRunStarted;
        playerControls.Movement.Run.canceled += OnRunCanceled;
    }

    private void OnDisable()
    {
        playerControls.Disable();
        playerControls.Movement.Run.started -= OnRunStarted;
        playerControls.Movement.Run.canceled -= OnRunCanceled;
    }

    private void Update()
    {
        PlayerInput();
        // Move();
    }

    private void FixedUpdate()
    {
        Move();
        // AdjustPlayerFacingDirection();
        // FollowTarget();
    }

    // private void FollowTarget(){
    //     Vector3 desiredPosition = target.position + offset;
    //     Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothedSpeed);
    //     transform.position = smoothedPosition;
    // }

    // private void LateUpdate(){
    //     transform.position = target.position + offset;
    // }

    private void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();

        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);
    }

    private void Move()
    {
        Vector2 normalizedMovement = movement.normalized;
        float currentSpeed = isRunning ? runSpeed : moveSpeed;
        rb.MovePosition(rb.position + normalizedMovement * (currentSpeed * Time.fixedDeltaTime));
        
        AdjustPlayerFacingDirection();
    }

    private void OnRunStarted(InputAction.CallbackContext context)
    {
        isRunning = true;
    }

    private void OnRunCanceled(InputAction.CallbackContext context)
    {
        isRunning = false;
    }

    private void AdjustPlayerFacingDirection()
    {
        if (movement.x < 0)
        {
            mySpriteRenderer.flipX = true;
        }
        else if (movement.x > 0)
        {
            mySpriteRenderer.flipX = false;
        }
    }
}
