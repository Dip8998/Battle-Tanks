using UnityEngine;

namespace Assets.Scripts
{
    public class TankSelection : MonoBehaviour
    {
        [SerializeField] private TankSpawner tankSpawner;
        [SerializeField] private GameObject gameOverScreenObject;
        [SerializeField] private ScoreController scoreController;
        [SerializeField] private GameObject mainMenu;

        public void GreenTankSelection()
        {
            tankSpawner.CreateTank(TankTypes.GreenTank);
            ResetGame();
        }

        public void BlueTankSelection()
        {
            tankSpawner.CreateTank(TankTypes.BlueTank);
            ResetGame();
        }

        public void RedTankSelection()
        {
            tankSpawner.CreateTank(TankTypes.RedTank);
            ResetGame();
        }

        public void MainMenu()
        {
            mainMenu.gameObject.SetActive(true);
        }

        private void ResetGame()
        {
            DeactivateTankSelectionUI();
            DeactivateGameOverScreen();
            ResetScoreCounter();
        }

        private void DeactivateTankSelectionUI()
        {
            this.gameObject.SetActive(false);
        }

        private void DeactivateGameOverScreen()
        {
            if (gameOverScreenObject != null)
            {
                gameOverScreenObject.SetActive(false);
            }
            else
            {
                Debug.LogError("GameOver Screen Object not assigned in TankSelection!");
            }
        }

        private void ResetScoreCounter()
        {
            if (scoreController != null)
            {
                scoreController.ResetScore();
            }
            else
            {
                Debug.LogError("ScoreController not assigned in TankSelection!");
            }
        }
    }
}