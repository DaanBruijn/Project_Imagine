using UnityEngine;

public class vroomvroom : MonoBehaviour
{
    [SerializeField] private float _vroomSpeed;
    private Rigidbody _vroom;
    private bool _canBusMove;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _vroom = GetComponent<Rigidbody>();
        _canBusMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            _canBusMove = true;
        
        MoveBus();
    }

    void MoveBus()
    {
        if (_canBusMove)
            _vroom.AddForce(Vector3.left * _vroomSpeed,ForceMode.Impulse);
    }
}
