using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BombManager : MonoBehaviour
{
    public static BombManager Instance { get; private set; }
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private int bombPoolSize = 10;
    [SerializeField] private Player player;

    private bool isGameStarted = false;
    private bool isGameOver = false;
    private List<GameObject> bombPool = new List<GameObject>();
    private Dictionary<GameObject, int> bombTimers = new Dictionary<GameObject, int>();
    private int bombIdCounter = 0;

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
        PreloadBombs();
        GameManager.Instance.OnGameStarted += GameManager_OnGameStarted;
        GameManager.Instance.OnGameOver += GameManager_OnGameOver;
    }

    private void Update()
    {
        if (!isGameStarted || isGameOver)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            SpawnBomb();
        }
    }

    private void RestartGame()
    {
        isGameStarted = true;
        isGameOver = false;
        bombIdCounter = 0;
        bombTimers.Clear();
        foreach (GameObject bomb in bombPool)
        {
            bomb.SetActive(false);
        }
    }

    private void PreloadBombs()
    {
        for (int i = 0; i < bombPoolSize; i++)
        {
            GameObject bomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);
            bomb.SetActive(false);
            bombPool.Add(bomb);
        }
    }

    public void SpawnBomb()
    {
        GameObject bomb = GetPooledBomb();
        if (bomb != null)
        {
            Vector3 spawnPosition = player.GetPlayerPosition();
            bomb.transform.position = spawnPosition;
            bomb.SetActive(true);

            int token = ++bombIdCounter;
            bombTimers[bomb] = token;

            StartCoroutine(DeactivateBombAfterTime(bomb, 3f, token));
        }
    }

    private IEnumerator DeactivateBombAfterTime(GameObject bomb, float time, int token)
    {
        yield return new WaitForSeconds(time);

        if (bombTimers.TryGetValue(bomb, out int currentToken) && currentToken == token)
        {
            bomb.SetActive(false);
        }
    }

    private GameObject GetPooledBomb()
    {
        foreach (GameObject bomb in bombPool)
        {
            if (!bomb.activeInHierarchy)
                return bomb;
        }
        return null;
    }

    private void GameManager_OnGameStarted()
    {
        RestartGame();
    }

    private void GameManager_OnGameOver()
    {
        isGameOver = true;
    }
}
