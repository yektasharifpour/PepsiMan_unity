using UnityEngine;

public class ObstacleCleanup : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _player;
    [SerializeField] private ObstaclePool _obstaclePool;
    [Header("Cleanup Settings")]
    [SerializeField] private float _destroyBehindDistance = 10f;
  

    void Update()
    {
        if (_player == null) return;
        destroyIfBehindPlayer();
    }
    void destroyIfBehindPlayer()
    {
        if (transform.position.z < _player.position.z - _destroyBehindDistance)
        {
            _obstaclePool.returnObstacle(gameObject);
        }
    }
    public void setReferences(Transform player , ObstaclePool pool)
    {
        _player = player;
        _obstaclePool = pool;
    }
}
