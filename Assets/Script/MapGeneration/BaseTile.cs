using UnityEngine;

public class BaseTile : MonoBehaviour
{
    [SerializeField] private Transform[] _connectionPoints;
    public Transform[] connectionPoints
    {
        get { return _connectionPoints; }
    }
}
