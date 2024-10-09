using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public List<WallSpawnPoint> spawnPoints = new List<WallSpawnPoint>();
    [SerializeField] private bool _canRotate = true;
    public bool CanRotate { get { return _canRotate; } private set { _canRotate = value; } }
    public float Height 
    { 
        get { 
            if(spawnPoints.Count > 0  && spawnPoints.Count < 3)
                return Mathf.Abs(spawnPoints[1].transform.position.y) - Mathf.Abs(spawnPoints[0].transform.position.y); 
            return 0;
        } 
    }

    private void Start()
    {
        if (spawnPoints[0] == null)
        {
            Debug.LogError("Верхняя точка не задана!");
        }
        if (spawnPoints[1] == null)
        {
            Debug.LogError("Нижняя точка не задана!");
        }
    }
}
