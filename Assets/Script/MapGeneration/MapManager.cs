using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] private GameObject _startTile;
    [SerializeField] private GameObject[] _tiles;
    [SerializeField] private GameObject[] _endTiles;
    [SerializeField] private int _maxTileCount;
    private int _currentTiles;

    private void Start()
    {
        GenerateMap();
    }
    private void GenerateMap()
    {
        // - set first tile
        GameObject firstTile = Instantiate(_startTile);
        firstTile.transform.position = Vector3.zero;

        // - check how many connection points
        int connectionPoints = firstTile.GetComponent<BaseTile>().connectionPoints.Length;

        // - add tiles based on the connectionpoints
        for(int i = 0; i < connectionPoints; i++)
        {
            GameObject subTile = Instantiate(_tiles[Random.Range(0, _tiles.Length)]);
            subTile.transform.position = firstTile.GetComponent<BaseTile>().connectionPoints[i].position;
            subTile.transform.rotation = firstTile.GetComponent <BaseTile>().connectionPoints[i].rotation;
            subTile.transform.SetParent(firstTile.transform);
            _currentTiles++;
            RecursionFunction(subTile);
        }

    }

    private void RecursionFunction(GameObject tile)
    {
        int connectionPoints = tile.GetComponent<BaseTile>().connectionPoints.Length;
        if ((_currentTiles + connectionPoints) >= _maxTileCount )
        {
            for (int i = 0; i < connectionPoints; i++)
            {
                GameObject subTile = Instantiate(_endTiles[Random.Range(0, _tiles.Length)]);
                subTile.transform.position = tile.GetComponent<BaseTile>().connectionPoints[i].position;
                subTile.transform.rotation = tile.GetComponent<BaseTile>().connectionPoints[i].rotation;
                subTile.transform.SetParent(tile.transform);
                _currentTiles++;
                RecursionFunction(subTile);
            }
        }
        else
        {
            for (int i = 0; i < connectionPoints; i++)
            {
                GameObject subTile = Instantiate(_tiles[Random.Range(0, _tiles.Length)]);
                subTile.transform.position = tile.GetComponent<BaseTile>().connectionPoints[i].position;
                subTile.transform.rotation = tile.GetComponent<BaseTile>().connectionPoints[i].rotation;
                subTile.transform.SetParent(tile.transform);
                _currentTiles++;
                RecursionFunction(subTile);
            }
        }

    }
    private void Allign()
    {

    }
}
