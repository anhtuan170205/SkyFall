using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int enemyPoolSize = 20;
    [SerializeField] private float spawnRate = 0.005f;
    [SerializeField] private List<EnemySpawnPositionSO> enemySpawnPositionList;

    private Dictionary<GameObject, int> enemyTimers = new Dictionary<GameObject, int>();
    private int timerIdCounter = 0;
    private List<GameObject> enemyPool = new List<GameObject>();
    private float nextSpawn = 0f;

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
    }

    private void Update()
    {
        nextSpawn -= Time.deltaTime;
        if (nextSpawn <= 0f)
        {
            SpawnEnemy();
            nextSpawn = spawnRate;
        }
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

}
