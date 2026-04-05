using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using UnityEngine.InputSystem;
using System;
using Unity.VisualScripting;

// - Stalkerbehavior for the playtest. simply stalks and teleports if too visible on the screen dependent on the screen allowance.
// Switching the state to stalk enables the stalking behavior
public class PlaytestStalkerBehaviour : MonoBehaviour
{
    [SerializeField] private float _screenAllowance;
    [SerializeField] private float _stalkDistance;
    private Transform _cameraTransform;

    private NavMeshAgent _agent;
    public enum _States
    {
        roam = 0,
        stalk = 1,
        chase = 2, //these states are planned for future behaviour but rn i think him only being in the corners of your eyesight is scarier
        bendReality = 4,
        idle = 5
    }
    private _States _state;

    private void Awake()
    {
        _cameraTransform = Camera.main.transform;
        _agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {

    }

    public void SwitchState(string state)
    {
        _state = (_States)Enum.Parse(typeof(_States), state);
        switch (_state)
        {
            case _States.roam:
                Roam();
                break;
            case _States.stalk:
                StartCoroutine(Stalk());
                break;
            case _States.chase:
                StartCoroutine(Chase());
                break;
        }
    }
    public void DecreaseStalkDistance(float amount)
    {
        _stalkDistance -= amount;
    }
    public void IncreaseScreenAllowance(float amount)
    {
        _screenAllowance += amount;
    }
    private void Roam()
    {

    }

    private IEnumerator Stalk()
    {
        while (_state == _States.stalk)
        {
            _agent.SetDestination(_cameraTransform.position + (_cameraTransform.forward * -_stalkDistance));
            if (CheckIfInEyeDistance(transform.position))
            {
                _agent.SetDestination(transform.position);
            }
            else if (CheckIfOnScreen(_cameraTransform.position))
            {
                _agent.SetDestination(transform.position);
            }
            yield return null;
        }
        yield break;
    }
    private IEnumerator Chase()
    {
        while (_state == _States.chase)
        {
            _agent.SetDestination(_cameraTransform.position + (_cameraTransform.forward * -_stalkDistance));
            if (CheckIfInEyeDistance(transform.position))
            {
                _agent.Warp(_cameraTransform.position + (new Vector3(_cameraTransform.forward.x, 0, _cameraTransform.forward.z) * -_stalkDistance));
            }
            else if (CheckIfOnScreen(_cameraTransform.position))
            {
                _agent.SetDestination(transform.position);
            }
            yield return null;
        }
        yield break;
    }
    private bool CheckIfInEyeDistance(Vector3 position)
    {
        Vector3 onScreenPoint = Camera.main.WorldToScreenPoint(position);

        if (onScreenPoint.x > _screenAllowance && onScreenPoint.x < Screen.width - _screenAllowance && onScreenPoint.z >0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool CheckIfOnScreen(Vector3 position)
    {
        Vector3 onScreenPoint = Camera.main.WorldToScreenPoint(position);
        if (onScreenPoint.x > 0 &&  onScreenPoint.x < Screen.width && onScreenPoint.z > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void IncreaseSpeed(float speedVariable)
    {
        _agent.speed += speedVariable;
    }
}
