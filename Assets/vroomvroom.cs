using UnityEngine;

public class vroomvroom : MonoBehaviour
{
    [SerializeField] private float _vroomSpeed;
    private Rigidbody _vroom;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _vroom = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _vroom.AddForce(Vector3.forward * _vroomSpeed,ForceMode.Impulse);
    }
}
