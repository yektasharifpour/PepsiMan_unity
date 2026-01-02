using System.Collections.Generic;
using UnityEngine;



//  این اسکریپت سیستم آبجکت پولینگ موانع را پیاده‌سازی می‌کند تا به‌جای نابودسازی، موانع دوباره استفاده شوند.
//  This script implements object pooling for obstacles to reuse them instead of instantiating and destroying repeatedly.

public class ObstaclePool : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private List<GameObject> _obstaclePrefabs = new List<GameObject>();

    [Header("Pool Settings")]
    [SerializeField] private int _initialSizePerPrefab = 10;

    [Header("Hierarchy Parent")]
    [SerializeField] private Transform _obstacleParent;

    private readonly Dictionary<int, Queue<GameObject>> _poolsByIndex = new Dictionary<int, Queue<GameObject>>();
    private readonly List<GameObject> _allObstacles = new List<GameObject>();


    void Awake()
    {
        if (_obstaclePrefabs == null || _obstaclePrefabs.Count == 0) return;

        for (int prefabIndex = 0; prefabIndex < _obstaclePrefabs.Count; prefabIndex++)
        {
            if (_obstaclePrefabs[prefabIndex] == null) continue;

            if (!_poolsByIndex.ContainsKey(prefabIndex))
            {
                _poolsByIndex.Add(prefabIndex, new Queue<GameObject>());
            }

            for (int i = 0; i < _initialSizePerPrefab; i++)
            {
                GameObject obj = createNewObstacle(prefabIndex);
                obj.SetActive(false);
                _poolsByIndex[prefabIndex].Enqueue(obj);
            }
        }
    }


    GameObject createNewObstacle(int prefabIndex)
    {
        GameObject prefab = _obstaclePrefabs[prefabIndex];
        if (prefab == null) return null;

        GameObject obj;

        if (_obstacleParent != null)
        {
            obj = Instantiate(prefab, _obstacleParent);
        }
        else
        {
            obj = Instantiate(prefab);
        }

        ObstaclePoolMember member = obj.GetComponent<ObstaclePoolMember>();
        if (member == null)
        {
            member = obj.AddComponent<ObstaclePoolMember>();
        }
        member.setPrefabIndex(prefabIndex);

        _allObstacles.Add(obj);
        return obj;
    }


    public GameObject getObstacle()
    {
        if (_obstaclePrefabs == null || _obstaclePrefabs.Count == 0) return null;

        int prefabIndex = Random.Range(0, _obstaclePrefabs.Count);
        return getObstacle(prefabIndex);
    }


    public GameObject getObstacle(int prefabIndex)
    {
        if (_obstaclePrefabs == null || _obstaclePrefabs.Count == 0) return null;
        if (prefabIndex < 0 || prefabIndex >= _obstaclePrefabs.Count) return null;
        if (_obstaclePrefabs[prefabIndex] == null) return null;

        if (!_poolsByIndex.ContainsKey(prefabIndex))
        {
            _poolsByIndex.Add(prefabIndex, new Queue<GameObject>());
        }

        Queue<GameObject> pool = _poolsByIndex[prefabIndex];

        if (pool.Count == 0)
        {
            GameObject obj = createNewObstacle(prefabIndex);
            if (obj == null) return null;

            obj.SetActive(false);
            pool.Enqueue(obj);
        }

        GameObject obstacleObj = pool.Dequeue();
        obstacleObj.SetActive(true);
        return obstacleObj;
    }


    public void returnObstacle(GameObject obstacleObj)
    {
        if (obstacleObj == null) return;

        ObstaclePoolMember member = obstacleObj.GetComponent<ObstaclePoolMember>();
        if (member == null)
        {
            // If somehow missing, just disable it and don't crash
            obstacleObj.SetActive(false);
            return;
        }

        int prefabIndex = member.getPrefabIndex();
        if (!_poolsByIndex.ContainsKey(prefabIndex))
        {
            _poolsByIndex.Add(prefabIndex, new Queue<GameObject>());
        }

        obstacleObj.SetActive(false);
        _poolsByIndex[prefabIndex].Enqueue(obstacleObj);
    }


  

}



//  این کامپوننت کوچک مشخص می‌کند هر مانع متعلق به کدام Prefab/Pool است.
//  This small component stores which prefab/pool index this obstacle belongs to.
public class ObstaclePoolMember : MonoBehaviour
{
    [SerializeField] private int _prefabIndex = 0;

    public void setPrefabIndex(int index)
    {
        _prefabIndex = index;
    }

    public int getPrefabIndex()
    {
        return _prefabIndex;
    }
}
