using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject menuPanel; 
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI gameOverHighScoreText;
    [SerializeField] private TextMeshProUGUI gameOverScoreText;
    private void Start()
    {
        pausePanel.SetActive(false);
        menuPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        ScoreManager.Instance.OnScoreChanged += ScoreManager_OnScoreChanged;
        GameManager.Instance.OnGameStarted += GameManager_OnGameStarted;
        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        GameManager.Instance.OnGameResumed += GameManager_OnGameResumed;
        GameManager.Instance.OnReturnToMenu += GameManager_OnReturnToMenu;
        GameManager.Instance.OnGameOver += GameManager_OnGameOver;
    }
    private void ScoreManager_OnScoreChanged()
    {
        scoreText.text = "Score: " + ScoreManager.Instance.Score.ToString("00");
    }
    public void ShowPausePanel()
    {
        pausePanel.SetActive(true);

    }
    public void HidePausePanel()
    {
        pausePanel.SetActive(false);
    }
    public void ShowMenuPanel()
    {
        menuPanel.SetActive(true);
    }
    public void HideMenuPanel()
    {
        menuPanel.SetActive(false);
    }
    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
        gameOverHighScoreText.text = "BEST: " + ScoreManager.Instance.GetHighScore().ToString("00");
        gameOverScoreText.text = "SCORE: " + ScoreManager.Instance.GetScore().ToString("00");
    }
    public void HideGameOverPanel()
    {
        gameOverPanel.SetActive(false);
    }
    private void GameManager_OnGameStarted()
    {
        HideMenuPanel();
        HideGameOverPanel();
    }
    private void GameManager_OnGamePaused()
    {
        ShowPausePanel();
    }
    private void GameManager_OnGameResumed()
    {
        HidePausePanel();
    }

    private void GameManager_OnReturnToMenu()
    {
        HidePausePanel();
        HideGameOverPanel();
        ShowMenuPanel();
    }
    private void GameManager_OnGameOver()
    {
        ShowGameOverPanel();
    }

}
