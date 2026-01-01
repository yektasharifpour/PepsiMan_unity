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
    private readonly List<GameObject> _allObstacles = new List<GameObject>();


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
        GameObject obj;

        if (_obstacleParent != null)
        {
            obj = Instantiate(_obstaclePrefab, _obstacleParent);
        }
        else
        {
            obj = Instantiate(_obstaclePrefab);
        }

        _allObstacles.Add(obj);
        return obj;
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
    public void resetAllActive()
    {
        for (int i = 0; i < _allObstacles.Count; i++)
        {
            GameObject obj = _allObstacles[i];
            if (obj == null) continue;

            if (obj.activeSelf)
            {
                returnObstacle(obj);
            }
        }
    }

}
