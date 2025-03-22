using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private void Start()
    {
        ScoreManager.Instance.OnScoreChanged += ScoreManager_OnScoreChanged;
    }
    private void ScoreManager_OnScoreChanged()
    {
        scoreText.text = "Score: " + ScoreManager.Instance.Score.ToString("00");
    }
}
