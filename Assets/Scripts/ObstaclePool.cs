using System.Collections.Generic;
using UnityEngine;

public class ObstaclePool : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] private GameObject _obstaclePrefab;

    [Header("Pool Settings")]
    [SerializeField] private int _initialSize = 20;

    [Header("Hierarchy Parent")]
    [SerializeField] private Transform _obstacleParent;

    private readonly Queue<GameObject> _pool = new Queue<GameObject>();

    void Awake()
    {
        if (_obstaclePrefab == null) return;

        for (int i = 0; i < _initialSize; i++)
        {
            GameObject obj = createNewObstacle();
            obj.SetActive(false);
            _pool.Enqueue(obj);
        }
    }

    GameObject createNewObstacle()
    {
        if (_obstacleParent != null)
        {
            return Instantiate(_obstaclePrefab, _obstacleParent);
        }

        return Instantiate(_obstaclePrefab);
    }

    public GameObject getObstacle()
    {
        if (_pool.Count == 0)
        {
            GameObject obj = createNewObstacle();
            obj.SetActive(false);
            _pool.Enqueue(obj);
        }

        GameObject obstacleObj = _pool.Dequeue();
        obstacleObj.SetActive(true);
        return obstacleObj;
    }

    public void returnObstacle(GameObject obstacleObj)
    {
        if (obstacleObj == null) return;

        obstacleObj.SetActive(false);
        _pool.Enqueue(obstacleObj);
    }
}
