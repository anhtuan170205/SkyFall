using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    public event Action OnScoreChanged;
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
        Bomb.OnBombExploded += AddScore;
    }
    private int score = 0;
    public int Score { get { return score; } }
    public void AddScore()
    {
        score++;
        OnScoreChanged?.Invoke();
    }
    public void ResetScore()
    {
        score = 0;
    }
}
