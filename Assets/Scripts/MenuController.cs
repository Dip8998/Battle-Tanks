using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject selectionPanel;
    [SerializeField] private Button playButton;

    private void Awake()
    {
        playButton.onClick.AddListener(StartGame);
    }

    public void StartGame()
    {
        gameObject.SetActive(false); 
        selectionPanel.SetActive(true); 
    }
}