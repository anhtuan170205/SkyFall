using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int enemyPoolSize = 20;
    [SerializeField] private float spawnRate = 1f;
    [SerializeField] private List<EnemySpawnPositionSO> enemySpawnPositionList;

    private Dictionary<GameObject, int> enemyTimers = new Dictionary<GameObject, int>();
    private int timerIdCounter = 0;
    private List<GameObject> enemyPool = new List<GameObject>();
    private float nextSpawn = 0f;
    private bool isGameStarted = false;
    private bool isGameOver = false;

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
        PreloadEnemies();
        ScoreManager.Instance.OnEveryXScore += ScoreManger_OnEveryXScore;
        GameManager.Instance.OnGameStarted += GameManager_OnGameStarted;
        GameManager.Instance.OnGameOver += GameManager_OnGameOver;
    }

    private void Update()
    {
        if (!isGameStarted || isGameOver)
        {
            return;
        }
        nextSpawn -= Time.deltaTime;
        if (nextSpawn <= 0f)
        {
            SpawnEnemy();
            nextSpawn = spawnRate;
        }
    }

    private void RestartGame()
    {
        isGameStarted = true;
        isGameOver = false;
    }

    private void PreloadEnemies()
    {
        for (int i = 0; i < enemyPoolSize; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            enemy.SetActive(false);
            enemyPool.Add(enemy);
        }
    }

    private void SpawnEnemy()
    {
        GameObject enemy = GetPooledEnemy();
        if (enemy != null && enemySpawnPositionList.Count > 0)
        {
            int randomIndex = Random.Range(0, enemySpawnPositionList.Count);
            Vector3 spawnPos = new Vector3(
                enemySpawnPositionList[randomIndex].enemyPositionX,
                enemySpawnPositionList[randomIndex].enemyPositionY,
                0
            );

            enemy.transform.position = spawnPos;
            enemy.SetActive(true);

            int token = ++timerIdCounter;
            enemyTimers[enemy] = token;

            StartCoroutine(DeactivateEnemyAfterTime(enemy, 8f, token));
        }
    }


    private GameObject GetPooledEnemy()
    {
        foreach (GameObject enemy in enemyPool)
        {
            if (!enemy.activeInHierarchy)
                return enemy;
        }
        return null;
    }
    private IEnumerator DeactivateEnemyAfterTime(GameObject enemy, float time, int token)
    {
        yield return new WaitForSeconds(time);

        if (enemyTimers.TryGetValue(enemy, out int currentToken) && currentToken == token)
        {
            enemy.SetActive(false);
        }
    }

    private void ScoreManger_OnEveryXScore()
    {
        spawnRate *= 0.75f;
    }
    private void GameManager_OnGameStarted()
    {
        RestartGame();
    }
    private void GameManager_OnGameOver()
    {
        isGameOver = true;
        timerIdCounter = 0;
        nextSpawn = 0f;
        spawnRate = 1;
        foreach (GameObject enemy in enemyPool)
        {
            enemy.SetActive(false);
        }
    }
}
