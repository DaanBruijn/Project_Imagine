using System.Collections;
using UnityEngine;

// - Simple PlayerMovement Script for the Playtest
// - (Needs to be edited for main game)
// - Daniel Bruijn

public class PlayerController : MonoBehaviour
{
    // - States
    public enum MovementState
    {
        walking,
        sprinting,
        air
    }

    // - Variables
    [Header("Movement")] 
    public float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public float groundDrag;

    [Header("Jump")] 
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;

    [Header("Keybinds")] 
    public KeyCode jumpkey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Ground Check")] 
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;

    [Header("Other Stuff")] 
    public Transform orientation;
    public PlayerCam cam;

    public MovementState state;
    public MovementState lastState;

    private float desiredMoveSpeed;
    private float lastDesiredMoveSpeed;

    private float horizontalInput;
    private float verticalInput;

    private bool readyToJump;
    private bool keepMomentum;

    private Vector3 moveDirection;
    private Rigidbody rb;

    private void Start()
    {
        // - set RB and CamFOV
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;
        cam.DoFov(80f);
    }

    private void Update()
    {
        // - Check if grounded
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        rb.linearDamping = grounded ? groundDrag : 0;

        MyInput();
        SpeedControl();
        StateHandler();
    }

    private void FixedUpdate()
    {
        // - Apply movement forces
        MovePlayer();
    }

    private void MyInput()
    {
        // - Get input axis and handles jumpkey
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpkey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void StateHandler()
    {
        // - Update movement state and speed
        if (grounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            desiredMoveSpeed = sprintSpeed;
            cam.DoFov(73f);
        }
        else if (grounded)
        {
            state = MovementState.walking;
            desiredMoveSpeed = walkSpeed;
            cam.DoFov(70f);
        }
        else
        {
            state = MovementState.air;
            desiredMoveSpeed = sprintSpeed;
            cam.DoFov(70f);
        }

        if (desiredMoveSpeed != lastDesiredMoveSpeed)
            moveSpeed = desiredMoveSpeed;

        lastDesiredMoveSpeed = desiredMoveSpeed;
        lastState = state;
    }

    private void MovePlayer()
    {
        // - Calculate move direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        else
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        // - Limit player speed if grounded
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        // - Reset vertical velocity
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        // - Allow jumping again
        readyToJump = true;
    }
}
