using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Forward Movement")]
    [SerializeField] private float _forwardSpeed = 8f;
    [Header("laneChangeSpeed")]
    [SerializeField] private float _LaneChangeSpeed = 10f;
    [SerializeField] private float _LaneOffSet = 2f;
    private int _laneIndex = 1;
    private bool _isDead = false;

    void Update()
    {
        if (_isDead) return;
        playerMovement();
    }
    void playerMovement()
    {
        transform.Translate(Vector3.forward * _forwardSpeed * Time.deltaTime, Space.World);
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            _laneIndex = Mathf.Min(2, _laneIndex + 1);
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _laneIndex = Mathf.Max(0, _laneIndex - 1);
        }
        float targetX = (_laneIndex - 1) * _LaneOffSet;
        Vector3 targetPos = new Vector3(targetX , transform.position.y,transform.position.z );
        transform.position = Vector3.MoveTowards(transform.position , targetPos , _LaneChangeSpeed*Time.deltaTime);
    }
    public void isDie()
    {
        _isDead = true;
    }
}
