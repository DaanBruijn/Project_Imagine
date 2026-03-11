using UnityEngine;

public class ChangeStalkerImage : MonoBehaviour
{
    [SerializeField] private GameObject _normalState;
    [SerializeField] private GameObject _chaseState;
    [SerializeField] private float _speedThreshold;
    private float _speed;
    private Vector3 _lastPosition;

    public void CheckSpeed()
    {
        _speed = (_lastPosition - transform.position).magnitude * 10;
        _lastPosition = transform.position;
    }

    private void Update()
    {
        CheckSpeed();
        if (_speed > _speedThreshold)
        {
           _chaseState.SetActive(true);
            _normalState.SetActive(false);
        }
        else if (_speed < _speedThreshold)
        {
            _chaseState.SetActive(false);
            _normalState.SetActive(true);
        }

    }
}
