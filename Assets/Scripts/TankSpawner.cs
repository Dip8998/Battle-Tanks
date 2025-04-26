using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Tank
    {
        public float moveSpeed;
        public float rotateSpeed;
        public TankTypes tankType;
        public Material color;
        public int health;
    }

    public List<Tank> tankList;
    public TankView tankViewPrefab;
    public EnemySpawner enemySpawner;
    public CameraController cam;
    public GameOverController gameOverScreen; 
    public PauseMenuController pauseMenuController; 
    private Transform playerTankTransform;

    public void CreateTank(TankTypes tankType)
    {
        TankView spawnedTankView = null;

        if (tankType == TankTypes.GreenTank)
        {
            TankModel tankModel = new TankModel(tankList[0].moveSpeed, tankList[0].rotateSpeed, tankList[0].tankType, tankList[0].color, tankList[0].health);
            TankController tankController = new TankController(tankModel, tankViewPrefab, cam);
            spawnedTankView = tankController.GetTankView();
        }
        else if (tankType == TankTypes.BlueTank)
        {
            TankModel tankModel = new TankModel(tankList[1].moveSpeed, tankList[1].rotateSpeed, tankList[1].tankType, tankList[1].color, tankList[1].health);
            TankController tankController = new TankController(tankModel, tankViewPrefab, cam);
            spawnedTankView = tankController.GetTankView();
        }
        else if (tankType == TankTypes.RedTank)
        {
            TankModel tankModel = new TankModel(tankList[2].moveSpeed, tankList[2].rotateSpeed, tankList[2].tankType, tankList[2].color, tankList[2].health);
            TankController tankController = new TankController(tankModel, tankViewPrefab, cam);
            spawnedTankView = tankController.GetTankView();
        }

        if (spawnedTankView != null)
        {
            playerTankTransform = spawnedTankView.transform;

            spawnedTankView.SetGameOverScreen(gameOverScreen);
            if (pauseMenuController != null)
            {
                pauseMenuController.SetCurrentTank(spawnedTankView.gameObject);
            }
            else
            {
                Debug.LogError("PauseMenuController reference not set in TankSpawner!");
            }

            if (enemySpawner != null)
            {
                enemySpawner.StartSpawning(playerTankTransform);
            }
            else
            {
                Debug.LogError("EnemySpawner reference not set in TankSpawner!");
            }
        }
        else
        {
            Debug.LogError("Failed to create TankView.");
        }
    }

    public void ResetEnemySpawner()
    {
        if (enemySpawner != null)
        {
            enemySpawner.StopSpawning();
            enemySpawner.ClearEnemies(); 
            enemySpawner.ResetSpawnTimer(); 
        }
    }
}