using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button backToSelectionButton;
    [SerializeField] private TankSpawner tankSpawner; 
    [SerializeField] private GameObject selectionPanel;

    private GameObject currentTank;
    private bool isGamePaused = false;

    private void Awake()
    {
        resumeButton.onClick.AddListener(ResumeGame);
        restartButton.onClick.AddListener(RestartLevel);
        backToSelectionButton.onClick.AddListener(GoToTankSelection);
        pauseButton.onClick.AddListener(PausePanelOpen);

        gameObject.SetActive(false);
    }

    public void SetCurrentTank(GameObject tank)
    {
        currentTank = tank;
    }

    public void PausePanelOpen()
    {
        this.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void TogglePauseMenu()
    {
        isGamePaused = !isGamePaused;
        Time.timeScale = isGamePaused ? 0f : 1f;
        gameObject.SetActive(isGamePaused);
    }

    public void ResumeGame()
    {
        isGamePaused = false;
        Time.timeScale = 1f;
        gameObject.SetActive(false);
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

        if (tankSpawner != null)
        {
            tankSpawner.ResetEnemySpawner();
        }

        if (selectionPanel != null)
        {
            selectionPanel.SetActive(true);
        }
        else
        {
            Debug.LogError("Selection Panel not assigned in PauseMenuController!");
        }

        gameObject.SetActive(false);

    }
}