using UnityEngine;
using UnityEngine.AI;

public class EnemyController
{
    private EnemyModel model;
    private EnemyView view;
    private Transform playerTransform; 
    private ScoreController scoreController;

    public EnemyController(EnemyModel _model, EnemyView _viewPrefab, Vector3 spawnPos, Transform player, ScoreController score) 
    {
        model = _model;
        view = GameObject.Instantiate(_viewPrefab, spawnPos, Quaternion.identity);
        playerTransform = player; 
        model.SetController(this);
        view.SetController(this);
        view.SetPlayerTransform(playerTransform); 
        scoreController = score;

        NavMeshAgent agent = view.GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.speed = model.moveSpeed;
        }
        else
        {
            Debug.LogError("NavMeshAgent not found on the enemy view!");
        }
    }

    public EnemyModel GetModel() => model;

    public void Fire()
    {
        view.FireBullet();
    }

    public void ReduceHealth(int amount)
    {
        model.health -= amount;
        if (model.health <= 0)
        {
            view.Die();
            scoreController.AddScore(10);
        }
    }
}