using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
    if (!isGameStarted || isGamePaused)
    {
        return;
    }

    mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    mousePosition.z = 0;

    float step = 1.0f;
    float offset = 0.5f;
    float roundedX = Mathf.Round((mousePosition.x - offset) / step) * step + offset;

    roundedX = Mathf.Clamp(roundedX, -1.5f, 1.5f);

    transform.position = new Vector3(roundedX, transform.position.y, 0);
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
            AudioManager.Instance.PlayGameOverClip();
        }
    }
    private void GameManager_OnGameStarted()
    {
        isGameStarted = true;
        isGamePaused = false;
    }
}
