using System;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    // - Variables
    [SerializeField] private float _moveSpeed;

    private enum Direction { Forward, Backward, Right, Left };
    [SerializeField] private Direction _direction; 
    
    private Rigidbody _rigidbody;
    private bool _canObjectMove;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _canObjectMove = false;
    }

    void Update()
    {
        MoveBus();
    }

    void MoveBus()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _canObjectMove = true;
            
            Vector3 moveDir = Vector3.zero;

            // - Uses the Enum Direction to decide what direction the RigidBody.Addforce will move
            switch (_direction)
            {
                case Direction.Forward:
                    moveDir = Vector3.forward;
                    break;

                case Direction.Backward:
                    moveDir = Vector3.back;
                    break;

                case Direction.Right:
                    moveDir = Vector3.right;
                    break;

                case Direction.Left:
                    moveDir = Vector3.left;
                    break;
            }

            _rigidbody.AddForce(moveDir * _moveSpeed * Time.deltaTime, ForceMode.Impulse);
        }
    }
}