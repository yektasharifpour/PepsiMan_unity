using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _player;
    [SerializeField] private GameObject _groundPrefab;

    [Header("Segment Settings")]
    [SerializeField] private float _segmentLength = 30f;
    [SerializeField] private int _segmentsAheadCount = 6;
    [SerializeField] private int _maxAliveSegments = 10;

    [Header("Hierarchy")]
    [SerializeField] private Transform _groundParent;


    private float _nextSpawnZ = 0f;
    private readonly Queue<GameObject> _aliveSegments = new Queue<GameObject>();
    void Start()
    {
        if (_player == null || _groundPrefab == null) return;
        _nextSpawnZ = 0f;
        for (int i = 0; i < _segmentsAheadCount; i++)
        {
            spawnOneSegment();
        }
    }

    void Update()
    {
        if (_player == null || _groundPrefab == null) return;
        spawnLoop();
    }
    void spawnLoop()
    {
        
        float playerAheadZ = _player.position.z + (_segmentsAheadCount * _segmentLength);

        while (_nextSpawnZ < playerAheadZ)
        {
            spawnOneSegment();
        }

      
        while (_aliveSegments.Count > _maxAliveSegments)
        {
            GameObject oldSeg = _aliveSegments.Dequeue();
            Destroy(oldSeg);
        }
    }
    void spawnOneSegment()
    {
        Vector3 spawnPos = new Vector3(0f, 0f, _nextSpawnZ);
        GameObject seg = Instantiate(_groundPrefab, spawnPos, Quaternion.identity,_groundParent);
        _aliveSegments.Enqueue(seg);

        _nextSpawnZ += _segmentLength;
    }
}
