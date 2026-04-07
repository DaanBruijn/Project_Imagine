using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
// - map generation, takes given tiles and connects them to eachother
// still needs cleanup
public class MapManager : MonoBehaviour
{
    [SerializeField] private GameObject _startTile;
    [SerializeField] private GameObject[] _tiles;
    [SerializeField] private GameObject[] _endTiles;
    [SerializeField] private GameObject _finalTile;
    [SerializeField] private int _minTileCount;
    [SerializeField] private int _waveCount;
    [SerializeField] private float _endTileLength;
    public List<GameObject> spawnedTiles;
    public List<GameObject> currentWave;
    private List<GameObject> _endingtiles;
    private int _currentTiles;

    private InputAction _generate;



    private void Awake()
    {
        _endingtiles = new List<GameObject>();
        _generate = InputSystem.actions.FindAction("Attack");
        GenerateMapTwo();
        while (_currentTiles < _minTileCount)
        {
            _currentTiles = 0;
            for (int i = 0; i < spawnedTiles.Count; i++)
            {
                Destroy(spawnedTiles[i]);
            }
            spawnedTiles.Clear();
            _endingtiles.Clear();
            currentWave.Clear();
            Debug.Log(spawnedTiles.Count);
            if (spawnedTiles.Count == 0)
            {
                GenerateMapTwo();
            }
        }
    }
    private void Update()
    {

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

    private void GenerateMapTwo()
    {
        GameObject firstTile = Instantiate(_startTile);
        firstTile.transform.position = Vector3.zero;
        spawnedTiles.Add(firstTile);

        // - check how many connection points
        int connectionPoints = firstTile.GetComponent<BaseTile>().connectionPoints.Length;
        for (int i = 0; i < connectionPoints; i++)
        {
            GameObject subTile = Instantiate(_tiles[Random.Range(0, _tiles.Length)]);
            subTile.transform.position = firstTile.GetComponent<BaseTile>().connectionPoints[i].position;
            subTile.transform.rotation = firstTile.GetComponent<BaseTile>().connectionPoints[i].rotation;
            subTile.transform.SetParent(firstTile.transform);
            spawnedTiles.Add(subTile);
            currentWave.Add(subTile);
            _currentTiles++;
        }
        for (int i = 0; i < _waveCount; i++)
        {
            AddGenerationWave();
        }
        int endingTile = Random.Range(0, _endingtiles.Count);
        GameObject final = Instantiate(_finalTile);
        final.transform.position = _endingtiles[endingTile].gameObject.transform.position;
        final.transform.rotation = _endingtiles[endingTile].gameObject.transform.rotation;
        Destroy(_endingtiles[endingTile]);
        spawnedTiles.Add(final);

    }

    private void AddGenerationWave()
    {
        List<GameObject> nextWave;
        nextWave = new List<GameObject>();
        for (int i = 0; i <currentWave.Count; i++)
        {

            for (int j = 0; j < currentWave[i].gameObject.GetComponent<BaseTile>().connectionPoints.Length; j++)
            {
                GameObject subTile = Instantiate(_tiles[Random.Range(0, _tiles.Length)]);
                subTile.transform.position = currentWave[i].gameObject.GetComponent<BaseTile>().connectionPoints[j].position;
                subTile.transform.rotation = currentWave[i].gameObject.GetComponent<BaseTile>().connectionPoints[j].rotation;
                subTile.transform.SetParent(currentWave[i].transform);
                if (CheckIfInterSectCustom(subTile.GetComponent<BaseTile>(), currentWave[i].gameObject))
                {
                    Destroy(subTile.gameObject);
                    GameObject endTile = Instantiate(_endTiles[Random.Range(0, _endTiles.Length)]);
                   endTile.transform.position = currentWave[i].GetComponent<BaseTile>().connectionPoints[j].position;
                    endTile.transform.rotation = currentWave[i].GetComponent<BaseTile>().connectionPoints[j].rotation;
                    endTile.transform.SetParent(currentWave[i].transform);
                    spawnedTiles.Add(endTile);
                    _currentTiles++;
                    _endingtiles.Add(endTile);
                }
                else
                {
                    spawnedTiles.Add(subTile);
                    _currentTiles++;
                    nextWave.Add(subTile);
                }
            }
        }
        currentWave.Clear();
        for (int i = 0; i <nextWave.Count; i++) 
        {
            currentWave.Add(nextWave[i]);
        }
    }


    private void RecursionFunction(GameObject tile)
    {
        int connectionPoints = tile.GetComponent<BaseTile>().connectionPoints.Length;
        if ((_currentTiles + connectionPoints) >= _minTileCount )
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
