using UnityEngine;




// این اسکریپت دوربین را با یک فاصله مشخص به‌صورت نرم دنبال می‌کند تا بازیکن همیشه در مرکز دید باشد.
//  This script smoothly follows the player using a fixed offset to keep the runner always in view.

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
