using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
// - map generation, takes given tiles and connects them to eachother
// still needs cleanup
public class MapManager : MonoBehaviour
{
    [SerializeField] private GameObject _startTile;
    [SerializeField] private GameObject[] _tiles;
    [SerializeField] private GameObject[] _endTiles;
    [SerializeField] private int _maxTileCount;
    [SerializeField] private float _endTileLength;
    public List<GameObject> spawnedTiles;
    private int _currentTiles;

    private InputAction _generate;



    private void Start()
    {
        _generate = InputSystem.actions.FindAction("Attack");
        GenerateMap();
    }
    private void Update()
    {
        if (_generate.WasPressedThisFrame())
        {
            _currentTiles = 0;
            for (int i = 0; i < spawnedTiles.Count; i++) 
            {
                Destroy(spawnedTiles[i]);
            }
            spawnedTiles.Clear();
            Debug.Log(spawnedTiles.Count);
            if (spawnedTiles.Count == 0)
            {
                GenerateMap();
            }
        }
    }
    private void GenerateMap()
    {
        // - set first tile
        GameObject firstTile = Instantiate(_startTile);
        firstTile.transform.position = Vector3.zero;
        spawnedTiles.Add(firstTile);

        // - check how many connection points
        int connectionPoints = firstTile.GetComponent<BaseTile>().connectionPoints.Length;

        // - add tiles based on the connectionpoints
        for(int i = 0; i < connectionPoints; i++)
        {
            GameObject subTile = Instantiate(_tiles[Random.Range(0, _tiles.Length)]);
            subTile.transform.position = firstTile.GetComponent<BaseTile>().connectionPoints[i].position;
            subTile.transform.rotation = firstTile.GetComponent <BaseTile>().connectionPoints[i].rotation;
            subTile.transform.SetParent(firstTile.transform);
            spawnedTiles.Add(subTile);
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
                GameObject endTile = Instantiate(_endTiles[Random.Range(0, _endTiles.Length)]);
                endTile.transform.position = tile.GetComponent<BaseTile>().connectionPoints[i].position;
                endTile.transform.rotation = tile.GetComponent<BaseTile>().connectionPoints[i].rotation;
                endTile.transform.SetParent(tile.transform);
                spawnedTiles.Add(endTile);
                _currentTiles++;
            }
            Debug.Log("done generating");
        }
        else
        {
            for (int i = 0; i < connectionPoints; i++)
            {
                GameObject subTile = Instantiate(_tiles[Random.Range(0, _tiles.Length)]);
                subTile.transform.position = tile.GetComponent<BaseTile>().connectionPoints[i].position;
                subTile.transform.rotation = tile.GetComponent<BaseTile>().connectionPoints[i].rotation;
                if (CheckIfInterSectCustom(subTile.GetComponent<BaseTile>(), tile))
                {
                    Destroy(subTile.gameObject);
                    GameObject endTile = Instantiate(_endTiles[Random.Range(0, _endTiles.Length)]);
                    endTile.transform.position = tile.GetComponent<BaseTile>().connectionPoints[i].position;
                    endTile.transform.rotation = tile.GetComponent<BaseTile>().connectionPoints[i].rotation;
                    endTile.transform.SetParent(tile.transform);
                    spawnedTiles.Add(endTile);
                    _currentTiles++;
                }
                else
                {
                    subTile.transform.position = tile.GetComponent<BaseTile>().connectionPoints[i].position;
                    subTile.transform.rotation = tile.GetComponent<BaseTile>().connectionPoints[i].rotation;
                    subTile.transform.SetParent(tile.transform);
                    spawnedTiles.Add(subTile);
                    _currentTiles++;
                    RecursionFunction(subTile);
                }
            }
        }

    }

    private bool CheckIfInterSectCustom(BaseTile tile,GameObject originTile)
    {
        for (int i = 0;i <spawnedTiles.Count; i++) 
        {
            if (spawnedTiles[i] == originTile)
            {
                return false;
            }
            if (tile.furthestPointMagnitude < (spawnedTiles[i].transform.position - tile.transform.position).magnitude + spawnedTiles[i].GetComponent<BaseTile>().furthestPointMagnitude)
            {
                if (Vector3.Dot((tile.transform.position - spawnedTiles[i].transform.position).normalized , tile.transform.forward) > 0)
                {
                    return false;
                }
                if (spawnedTiles[i].GetComponent<BaseTile>().EndTile)
                {
                    return false;
                }
                return true;
            }
        }
        return false;
    }

}
