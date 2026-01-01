using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform _target;
    [Header("Offset")]
    [SerializeField] private Vector3 _offSet = new Vector3(0f,4f,-8f);
    [Header("Follow Speed")]
    [SerializeField] private float _followSpeed = 10f;
 

    void LateUpdate()
    {
        if (_target == null ) return;
        cameraMovement();

    }
    void cameraMovement()
    {
        Vector3 targetPos = _target.position + _offSet;
        transform.position = Vector3.Lerp(transform.position, targetPos , _followSpeed*Time.deltaTime);
    }
}
