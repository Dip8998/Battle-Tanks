using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button tankSelectionButton;
    [SerializeField] private GameObject selectionPanel;
    [SerializeField] private TankSpawner tankSpawner;
    private GameObject currentTank;

    private void Awake()
    {
        restartButton.onClick.AddListener(RestartLevel);
        tankSelectionButton.onClick.AddListener(GoToTankSelection);
    }

    public void SetCurrentTank(GameObject tank)
    {
        currentTank = tank;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToTankSelection()
    {
        Time.timeScale = 1f;
        if (currentTank != null)
        {
            Destroy(currentTank);
            currentTank = null;
        }

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }

        if (selectionPanel != null)
        {
            selectionPanel.SetActive(true);
        }
        else
        {
            Debug.LogError("Selection Panel not assigned in GameOverController!");
        }

        gameObject.SetActive(false); 

        if (tankSpawner != null)
        {
            tankSpawner.ResetEnemySpawner();
        }
    }

}