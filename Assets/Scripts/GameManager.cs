using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;




// این اسکریپت مدیریت کلی بازی را بر عهده دارد؛ شامل امتیاز، بهترین رکورد، وضعیت  و راه‌اندازی مجدد بازی و گیم اور.
//  This script manages the overall game state including score, best record, game over UI, and scene restart.

public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Transform _playerTransform;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _bestText;
    [SerializeField] private GameObject _gameOverPanel;

    [Header("Score")]
    [SerializeField] private float _scorePerSecond = 1f;

    [Header("Pool")]
    [SerializeField] private ObstaclePool _obstaclePool;


    private float _scoreTimer = 0f;
    private int _score = 0;
    private int _bestScore = 0;
    private bool _isGameOver = false;

    void Start()
    {
        _bestScore = PlayerPrefs.GetInt("Best Score" , 0);
        updateUI();
        setGameOverPanel(false);
    }

    void Update()
    {
        if(_isGameOver) return;
        updateScore();
        updateBestScoreLive();
        updateUI();
    }
    void updateScore()
    {
        _scoreTimer += Time.deltaTime;
        if (_scoreTimer >= 1f/_scorePerSecond)
        {
            _scoreTimer = 0f;
            _score++;

        }
    }
    void updateBestScoreLive()
    {
        if (_score <= _bestScore) return;
        _bestScore = _score;
        PlayerPrefs.SetInt("Best Score" , _bestScore);
        PlayerPrefs.Save();
    }

    void updateUI()
    {
        if (_scoreText != null)
        {
            _scoreText.text = $"Score: {_score}";
        }
        if (_bestText != null)
        {
            _bestText.text = $"Best: {_bestScore}";
        }
    }
    void setGameOverPanel(bool isActive)
    {
        if (_gameOverPanel != null)
        {
            _gameOverPanel.SetActive(isActive);
        }
    }
    public void triggerGameOver()
    {
        _isGameOver = true;
        setGameOverPanel(true);
    
    }
    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public bool isGameOver()
    {
        return _isGameOver;
    }

}
