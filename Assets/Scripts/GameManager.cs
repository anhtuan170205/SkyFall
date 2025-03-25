using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
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

    public event Action OnGameStarted;
    public event Action OnGamePaused;
    public event Action OnGameResumed;
    public event Action OnReturnToMenu;
    public event Action OnGameOver;

    private void Start()
    {
        Time.timeScale = 0;
    }
    public void StartGame()
    {
        OnGameStarted?.Invoke();
        Time.timeScale = 1;
    }
    public void PauseGame()
    {
        OnGamePaused?.Invoke();
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        OnGameResumed?.Invoke();
        Time.timeScale = 1;
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is quitting");
    }
    public void ReturnToMenu()
    {
        OnGameOver?.Invoke();
        OnReturnToMenu?.Invoke();
        Time.timeScale = 0;
    }
    public void GameOver()
    {
        OnGameOver?.Invoke();
        Time.timeScale = 0;
    }
}
