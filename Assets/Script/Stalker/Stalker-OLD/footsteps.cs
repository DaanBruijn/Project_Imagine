using System.Collections;
using UnityEngine;

public class footsteps : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _speedThreshold;
    private float _speed;
    private Vector3 _lastPosition;
    private bool _playing;

    public void CheckSpeed()
    {
        _speed = (_lastPosition - transform.position).magnitude * 10;
        _lastPosition = transform.position;
    }

    private void Update()
    {
        CheckSpeed();
        if (_speed > _speedThreshold && !_playing)
        {
            _playing = true;
            _audioSource.Play();
            _audioSource.loop = true;
        }
        else if (_speed < _speedThreshold)
        {
            _playing = false;
            _audioSource.Stop();
        }

    }

}
