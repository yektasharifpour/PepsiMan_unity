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
    [Header("Pool")]
    [SerializeField] private ObstaclePool _obstaclePool;



    private float _nextSpawnZ = 0f;
    void Start()
    {
        if (_player == null) return;
        _nextSpawnZ = _player.position.z + _spawnAheadDistance;
        
    }

    void Update()
    {
        if (_player == null || _obstaclePool == null) return;
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
        GameObject obstacleObj = _obstaclePool.getObstacle();
        obstacleObj.transform.position = posToSpawn;
        obstacleObj.transform.rotation = Quaternion.identity;
        ObstacleCleanup cleanup = obstacleObj.GetComponent<ObstacleCleanup>();
        if (cleanup != null)
        {
            cleanup.setReferences(_player, _obstaclePool);
        }

    }
}
