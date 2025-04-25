// EnemySpawner.cs
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyView enemyPrefab;
    [SerializeField] private float spawnInterval = 5f;
    [SerializeField] private Transform[] spawnPoints;
    public ScoreController scoreController;
    private bool canSpawn = false;
    private Transform playerTransform; 


    public void StartSpawning(Transform player)
    {
        playerTransform = player;
        canSpawn = true;
        InvokeRepeating(nameof(SpawnEnemy), 1f, spawnInterval);
        Debug.Log("Enemy spawning started, targeting player: " + playerTransform?.name);
    }

    void SpawnEnemy()
    {
        if (!canSpawn) return;

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        EnemyModel model = new EnemyModel(3f, 2f, 100);
        EnemyController controller = new EnemyController(model, enemyPrefab, spawnPoint.position, playerTransform,scoreController); 

        if (playerTransform == null)
        {
            Debug.LogWarning("Player Transform is null when spawning enemy.");
        }
    }
}