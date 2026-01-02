using UnityEngine;



// این اسکریپت کنترل انیمیشن‌های بازیکن (Run / Idle) را بر اساس وضعیت بازی بر عهده دارد.
// This script controls the player's animations (Run / Idle) based on the game state.

public class PlayerAnimationController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator _animator;
    [SerializeField] private GameManager _gameManager;

    private readonly int _isRunningHash = Animator.StringToHash("isRunning");


    void Start()
    {
        if (_animator == null)
        {
            _animator = GetComponent<Animator>();
        }

        setRun(true);
    }


    void Update()
    {
        if (_gameManager == null) return;

        if (_gameManager.isGameOver())
        {
            setRun(false);
        }
    }


    void setRun(bool isRunning)
    {
        if (_animator == null) return;
        _animator.SetBool(_isRunningHash, isRunning);
    }
}
