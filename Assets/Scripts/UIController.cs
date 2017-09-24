using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

    private GameObject ActivePanel;
    public GameObject PanelMainMenu;
    public GameObject PanelPauseMenu;
    public GameObject PanelChooseLevelMenu;

    // Use this for initialization
    void Start ()
    {
        //The first panel to display is the MainMenu:
        ActivePanel = PanelMainMenu;
        SetActivePage(PanelMainMenu);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void SetActivePage(GameObject panelToActivate)
    {
        //Set the current panel to false:
        ActivePanel.SetActive(false);

        //Set the panel to display at true:
        ActivePanel = panelToActivate;
        if (panelToActivate != null)
        {
            panelToActivate.SetActive(true);
        }
    }

    // MAIN MENU:
    public void clickButtonPlay()
    {
        SetActivePage(PanelChooseLevelMenu);
    }

    public void clickButtonExit()
    {
        
    }

    // CHOOSE LEVEL MENU:
    public void clickButtonPlayLaunch()
    {
        SetActivePage(null);
    }

    public void clickButtonBack()
    {
        SetActivePage(PanelMainMenu);
    }
}
