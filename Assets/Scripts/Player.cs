using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 mousePosition;
    private bool isGameStarted = false;
    private bool isGamePaused = false;

    private void Start()
    {
        GameManager.Instance.OnGameStarted += GameManager_OnGameStarted;
        GameManager.Instance.OnGamePaused += () => isGamePaused = true;
        GameManager.Instance.OnGameResumed += () => isGamePaused = false;
        GameManager.Instance.OnGameOver += () => isGameStarted = false;
    }
    private void Update()
    {
        UpdatePlayerPosition();
    }

    private void UpdatePlayerPosition()
    {
        if (!isGameStarted)
        {
            return;
        }
        if (isGamePaused)
        {
            return;
        }
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        int clampedX = Mathf.Clamp(Mathf.RoundToInt(mousePosition.x), -2, 2);
        transform.position = new Vector3(clampedX, transform.position.y, 0);
    }

    public Vector3 GetPlayerPosition()
    {
        return transform.position;
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Enemy"))
        {
            GameManager.Instance.GameOver();
        }
    }
    private void GameManager_OnGameStarted()
    {
        isGameStarted = true;
        isGamePaused = false;
    }
}
