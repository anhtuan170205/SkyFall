using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int everyXScore = 20;
    private const int MAX_HIGH_SCORES = 5;
    public static ScoreManager Instance { get; private set; }
    public event Action OnScoreChanged;
    public event Action OnEveryXScore;
    private int highScore;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        Bomb.OnBombExploded += Bomb_OnBombExploded;
        GameManager.Instance.OnGameStarted += GameManager_OnGameStarted;
        GameManager.Instance.OnGameOver += GameManager_OnGameOver;
    }
    private int score = 0;
    public int Score { get { return score; } }

    private void Bomb_OnBombExploded()
    {
        AddScore();
    }
    public void AddScore()
    {
        score++;
        if (score % everyXScore == 0)
        {
            OnEveryXScore?.Invoke();
        }
        OnScoreChanged?.Invoke();
    }
    public void ResetScore()
    {
        score = 0;
        OnScoreChanged?.Invoke();
    }
    private void GameManager_OnGameStarted()
    {
        ResetScore();
    }
    private void GameManager_OnGameOver()
    {
        if (score > highScore)
        {
            highScore = score;
        }
    }
    public int GetScore()
    {
        return score;
    }
    public int GetHighScore()
    {
        return highScore;
    }
}
