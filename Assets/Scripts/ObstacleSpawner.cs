using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _player;
    [SerializeField] private GameObject _obstaclePrefab;

    [Header("Spawn Distance")]
    [SerializeField] private float _spawnAheadDistance = 30f;
    [SerializeField] private float _minGap = 3f;
    [SerializeField] private float _maxGap = 7f;

    [Header("lanes")]
    [SerializeField] private float _laneOffset = 2f;

    [Header("Hierarchy")]
    [SerializeField] private Transform _obstacleParent;


    private float _nextSpawnZ = 0f;
    void Start()
    {
        if (_player == null) return;
        _nextSpawnZ = _player.position.z + _spawnAheadDistance;
        
    }

    void Update()
    {
        if(_player == null || _obstaclePrefab == null) return;
        spawnObstacleLoop();
    }

    void spawnObstacleLoop()
    {
        if (_player.position.z + _spawnAheadDistance < _nextSpawnZ) return;
        spawnOneObstacle();
        float gap = Random.Range(_minGap, _maxGap);
        _nextSpawnZ += gap;
    }

    void spawnOneObstacle()
    {
        int laneIndex = Random.Range(0,3);
        float x = (laneIndex - 1) * _laneOffset;
        Vector3 posToSpawn = new Vector3(x,0.5f,_nextSpawnZ);
        GameObject obstacleObj = Instantiate(_obstaclePrefab, posToSpawn, Quaternion.identity,_obstacleParent);

        ObstacleCleanup cleanup = obstacleObj.GetComponent<ObstacleCleanup>();
        if (cleanup != null)
        {
            cleanup.setPlayer(_player);
        }

    }
}
