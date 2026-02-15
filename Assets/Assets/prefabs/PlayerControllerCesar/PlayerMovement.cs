using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    public CharacterController characterController;
    InputAction movement;
    private Vector3 PlayerVelocity;
    private bool IsGrounded;
    private float gravity = -9.8f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movement = InputSystem.actions.FindAction("Move");
    }

    // Update is called once per frame
    void Update()
    {
        IsGrounded = characterController.isGrounded;
        if (IsGrounded && PlayerVelocity.y < 0)
        {
            PlayerVelocity.y = -2f;
        }
        PlayerVelocity.y += gravity * Time.deltaTime;
        Vector2 input = movement.ReadValue<Vector2>();
        characterController.Move((((transform.forward * input.y) + (transform.right * input.x)).normalized * movementSpeed) * Time.deltaTime);
        characterController.Move((transform.up * PlayerVelocity.y) * Time.deltaTime);
    }
}
