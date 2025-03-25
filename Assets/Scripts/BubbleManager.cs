using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleManager : MonoBehaviour
{
    public static BubbleManager Instance { get; private set; }

    [SerializeField] private GameObject bubblePrefab;
    [SerializeField] private int bubblePoolSize = 30;
    [SerializeField] private float spawnInterval = 0.3f;
    [SerializeField] private Vector2 xRange = new Vector2(-1.5f, 1.5f);
    [SerializeField] private float spawnY = -5f;

    private List<GameObject> bubblePool = new List<GameObject>();
    private float nextSpawnTime = 0f;
    private bool isSpawning = false;

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
        PreloadBubbles();
        isSpawning = true;
    }

    private void Update()
    {
        if (!isSpawning)
            return;

        nextSpawnTime -= Time.deltaTime;
        if (nextSpawnTime <= 0f)
        {
            SpawnBubble();
            nextSpawnTime = spawnInterval;
        }
    }

    private void PreloadBubbles()
    {
        for (int i = 0; i < bubblePoolSize; i++)
        {
            GameObject bubble = Instantiate(bubblePrefab, transform.position, Quaternion.identity);
            bubble.SetActive(false);
            bubblePool.Add(bubble);
        }
    }

    private void SpawnBubble()
    {
        GameObject bubble = GetPooledBubble();
        if (bubble != null)
        {
            float randomX = Random.Range(xRange.x, xRange.y);
            Vector3 spawnPos = new Vector3(randomX, spawnY, 0f);

            bubble.transform.position = spawnPos;
            bubble.SetActive(true);
        }
    }

    private GameObject GetPooledBubble()
    {
        foreach (GameObject bubble in bubblePool)
        {
            if (!bubble.activeInHierarchy)
                return bubble;
        }
        return null;
    }

    public void StopSpawning()
    {
        isSpawning = false;
        foreach (GameObject bubble in bubblePool)
        {
            bubble.SetActive(false);
        }
    }

    public void StartSpawning()
    {
        isSpawning = true;
    }
}
