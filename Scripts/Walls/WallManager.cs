using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Walls
{
    public List<Wall> walls;
    public int count;
}

public class WallManager : MonoBehaviour
{
    [SerializeField] private Transform _whereSpawn;
    [SerializeField] private List<Wall> _spawnedWalls = new List<Wall>();
    [SerializeField] private List<Walls> _wallsToSpawn;
    [SerializeField] private int _showWallsPerOnce = 3;
    [SerializeField] private float _offsetY;
    private int _currentWallNumber = 0;
    private float _currentWallsPositionY = 0;
    private void Start()
    {
        Initialize();
    }
    private void Initialize()
    {
        SpawnLevels();
        DisableAllSpawnedWalls();
        ShowSomeWalls(_showWallsPerOnce);
    }
    private void Update()
    {
        _currentWallsPositionY = _whereSpawn.transform.position.y;
        if (_currentWallNumber < _spawnedWalls.Count)
        {
            Wall currentWall = _spawnedWalls[_currentWallNumber];
            Debug.Log(currentWall.transform.position + " " + currentWall.transform.localPosition);
            if ((Mathf.Abs(_currentWallsPositionY)) >= Mathf.Abs(currentWall.transform.localPosition.y) + _offsetY)
            {
                currentWall.gameObject.SetActive(false);
                _currentWallNumber++;
                ShowSomeWalls(_showWallsPerOnce);
            }
        }
    }

    public void SpawnLevels()
    {
        foreach (var wallToSpawn in _wallsToSpawn)
        {
            SpawnWalls(wallToSpawn.walls, wallToSpawn.count);
        }
    }
    public void SpawnWalls(List<Wall> walls, int count)
    {
        if (walls.Count == 0)
        {
            Debug.Log("Нет префабов для спавна!");
            return;
        }
        int currentCountOfSpawn = count;
        while (currentCountOfSpawn > 0)
        {
            Wall wall = GetRandomWall(walls);
            Quaternion wallRotate = wall.transform.rotation;

            if (wall.CanRotate)
            {
                int randomNumberRotate = UnityEngine.Random.Range(0, 5);
                wallRotate = Quaternion.Euler(wall.transform.rotation.x, 90 * randomNumberRotate, wall.transform.rotation.z);

                if (currentCountOfSpawn == count)
                {
                    wallRotate = Quaternion.Euler(wall.transform.rotation.x, wall.transform.rotation.y, wall.transform.rotation.z);
                }
            }
            

            Wall newWall = Instantiate(wall, transform.position, wallRotate, _whereSpawn);
            
            Vector3 lastWallBottomSpawnPoint = GetPositionToSpawn(_spawnedWalls);
            Vector3 newWallTopSpawnPoint = newWall.spawnPoints[0].transform.position;
            Vector3 difference = newWallTopSpawnPoint - newWall.transform.position;
            newWall.transform.position = lastWallBottomSpawnPoint - difference;
            _spawnedWalls.Add(newWall);
            currentCountOfSpawn--;
        }
    }

    private Wall GetRandomWall(List<Wall> walls)
    {
        int randomWallNumber = UnityEngine.Random.Range(0, walls.Count);

        return walls[randomWallNumber];
    }

    private Vector3 GetPositionToSpawn(List<Wall> spawnedWalls)
    {
        if (spawnedWalls.Count == 0) { return Vector3.zero; }

        Vector3 bottomSpawnPointLastWall = spawnedWalls[spawnedWalls.Count - 1].spawnPoints[1].transform.position;

        return bottomSpawnPointLastWall;
    }

    private void ShowSomeWalls(int number)
    {
        for (int i = 0; i < number; i++)
        {
            if (_currentWallNumber + i >= _spawnedWalls.Count) break;

            _spawnedWalls[_currentWallNumber + i].gameObject.SetActive(true);
        }
    }
    private void DisableAllSpawnedWalls()
    {
        for(int i = 0; i < _spawnedWalls.Count; i++)
        {
            _spawnedWalls[i].gameObject.SetActive(false);
        }
    }
    private void Reset()
    {
        _spawnedWalls.RemoveRange(1, _spawnedWalls.Count - 1);
        _whereSpawn.position = Vector3.zero;
        int childCount = _whereSpawn.childCount;

        for(int i = 1; i < childCount; i++)
        {
            Destroy(_whereSpawn.GetChild(i).gameObject);
        }

        _currentWallNumber = 0;
    }

    private void OnEnable()
    {
        GameManager.OnGameReset += Reset;
        GameManager.OnGameReset += Initialize;
    }

    private void OnDisable()
    {
        GameManager.OnGameReset -= Reset;
        GameManager.OnGameReset -= Initialize;
    }
}