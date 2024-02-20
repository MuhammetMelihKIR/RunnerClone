using System;
using TMPro;
using UnityEngine;
public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ScoreText, HighScoreText;

    private const string highScoreKey = "HighScore";

    private Vector3 _target;
    
    private float _scoreMultiplier = 1f;
    private float _initialDistance;
    private float _currentDistance;
    private int _currentScore = 0;
    private int _highScore;
    
    private void OnEnable()
    {
        UIManager.OnGameStart += UIManager_OnGameStart;
        UIManager.OnGameReset += UIManager_OnGameReset;
    }
    private void OnDisable()
    {
        UIManager.OnGameStart -= UIManager_OnGameStart;
        UIManager.OnGameReset -= UIManager_OnGameReset;
    }
    private void Start()
    {
        UpdateHighScoreUI();
    }
    private void Update()
    {
        if (GameManager.Instance.IsGameState(GameState.InGame))
            ScoreUpdate();
    }
    private void ScoreUpdate()
    {
        _currentDistance = Vector3.Distance(transform.position, _target);
        _currentScore = -Mathf.RoundToInt((_initialDistance - _currentDistance) * _scoreMultiplier);
        UpdateScoreUI();
        
        if (_currentScore > _highScore)
        {
            _highScore = _currentScore;
            PlayerPrefs.SetInt(highScoreKey, _highScore);
            UpdateHighScoreUI();
        }
    }
    private void UpdateScoreUI()
    {
        ScoreText.text = _currentScore.ToString();
    }
    private void UpdateHighScoreUI()
    {
        _highScore = PlayerPrefs.GetInt(highScoreKey, 0);
        HighScoreText.text =  _highScore.ToString();
    }
    private void UIManager_OnGameStart()
    {
        _target = transform.position;
        Vector3 targetPosition = transform.position;
        _initialDistance = Vector3.Distance(transform.position, targetPosition);
        _highScore = PlayerPrefs.GetInt(highScoreKey);
        UpdateHighScoreUI();
        _currentScore = 0;
        UpdateScoreUI();
    }
    private void UIManager_OnGameReset()
    {
        UpdateHighScoreUI();
    }
}
