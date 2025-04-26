// EnemySpawner.cs
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyView enemyPrefab;
    [SerializeField] private float spawnInterval = 5f;
    [SerializeField] private Transform[] spawnPoints;
    public ScoreController scoreController;
    private bool canSpawn = false;
    private Transform playerTransform;
    private List<GameObject> spawnedEnemies = new List<GameObject>();
    private float currentSpawnInterval;

    private void Awake()
    {
        currentSpawnInterval = spawnInterval;
    }

    public void StartSpawning(Transform player)
    {
        playerTransform = player;
        canSpawn = true;
        InvokeRepeating(nameof(SpawnEnemy), 1f, spawnInterval);
    }

    void SpawnEnemy()
    {
        if (!canSpawn) return;

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        EnemyModel model = new EnemyModel(3f, 2f, 100);
        EnemyController controller = new EnemyController(model, enemyPrefab, spawnPoint.position, playerTransform, scoreController);
        EnemyView enemyView = controller.GetView();
        if (enemyView != null)
        {
            spawnedEnemies.Add(enemyView.gameObject);
        }

        if (playerTransform == null)
        {
            Debug.LogWarning("Player Transform is null when spawning enemy.");
        }
    }

    public void StopSpawning()
    {
        canSpawn = false;
        CancelInvoke(nameof(SpawnEnemy));
    }

    public void ClearEnemies()
    {
        foreach (GameObject enemy in spawnedEnemies)
        {
            if (enemy != null)
            {
                Destroy(enemy);
            }
        }
        spawnedEnemies.Clear();
    }

    public void ResetSpawnTimer()
    {
        spawnInterval = currentSpawnInterval;
        CancelInvoke(nameof(SpawnEnemy));
        if (canSpawn && playerTransform != null)
        {
            InvokeRepeating(nameof(SpawnEnemy), 1f, spawnInterval);
        }
    }
}