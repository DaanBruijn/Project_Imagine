
using UnityEngine;
// - main class for a tile in the mapmanager
public class BaseTile : MonoBehaviour
{
    [SerializeField] private Transform[] _connectionPoints;
    [SerializeField] private Transform _furthestPoint;
    [SerializeField] private float _furthestPointMagnitude;
    [SerializeField] private bool _endTile;

    public bool EndTile
    {
        get { return _endTile; }
    }
    public Transform furthestPoint
    {
        get { return _furthestPoint; }
    }

    public Transform[] connectionPoints
    {
        get { return _connectionPoints; }
    }
    public float furthestPointMagnitude
    {
        get { return _furthestPointMagnitude; }
    }

    private void Awake()
    {
       _furthestPoint = DetermineFurthestPoint();
       _furthestPointMagnitude = (_furthestPoint.position - transform.position).magnitude;
    }

    private Transform DetermineFurthestPoint()
    {
        float longestDistance = 0;
        Transform furthestPoint = transform;
        for (int i = 0; i < _connectionPoints.Length; i++)
        {
            float magnitudeBetweenPoint = (_connectionPoints[i].position - transform.position).magnitude;
            if (magnitudeBetweenPoint > longestDistance)
            {
                longestDistance = magnitudeBetweenPoint;
                furthestPoint = _connectionPoints[i];
            }

        }
        return furthestPoint;
    }
}
