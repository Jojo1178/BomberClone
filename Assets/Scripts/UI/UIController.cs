using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

    private GameObject ActivePanel;
    public GameObject PanelMainMenu;
    public GameObject PanelPauseMenu;
    public GameObject PanelChooseLevelMenu;
    public GameObject PanelIngameMenu;
    public GameObject PanelEndGameMenu;

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

    // NO TREATMENTS IN THIS CLASS, ONLY GUI SWAP

    // MAIN MENU:
    public void clickButtonPlay()
    {
        SetActivePage(PanelChooseLevelMenu);
    }

    public void clickButtonExit()
    {
        Debug.Log("BUTTON EXIT CLICKED");
    }

    // CHOOSE LEVEL MENU:
    public void clickButtonPlayLaunch()
    {
        SetActivePage(PanelIngameMenu);
    }

    public void clickButtonBack()
    {
        SetActivePage(PanelMainMenu);
    }

    // IN GAME MENU:
    public void clickButtonPause()
    {
        SetActivePage(PanelPauseMenu);
    }

    // PAUSE MENU:
    public void clickButtonResume()
    {
        SetActivePage(PanelIngameMenu);
    }

    //END GAME MENU:
    public void clickButtonPlayAgain()
    {
        SetActivePage(PanelMainMenu);
    }
    
    //EVENTS
    public void eventEndGame()
    {
        SetActivePage(PanelEndGameMenu);
    }
}
