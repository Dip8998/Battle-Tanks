using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject selectionPanel;
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        playButton.onClick.AddListener(MenuPanelOff);
        quitButton.onClick.AddListener(Quit);
    }

    public void MenuPanelOff()
    {
        this.gameObject.SetActive(false);
        selectionPanel.gameObject.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
