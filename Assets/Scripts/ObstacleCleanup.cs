using UnityEngine;

public class ObstacleCleanup : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _player;
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
            Destroy(gameObject);
        }
    }
    public void setPlayer(Transform player)
    {
        _player = player;
    }
}
