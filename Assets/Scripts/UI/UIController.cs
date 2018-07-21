using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

    private UIPanel ActivePanel;
    public UIPanel PanelMainMenu;
    public UIPanel PanelPauseMenu;
    public UIPanel PanelChooseLevelMenu;
    public UIPanel PanelIngameMenu;
    public UIPanel PanelEndGameMenu;

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

    private void SetActivePage(UIPanel panelToActivate)
    {
        //Set the current panel to false:
        ActivePanel.gameObject.SetActive(false);

        //Set the panel to display at true:
        ActivePanel = panelToActivate;
        if (panelToActivate != null)
        {
            panelToActivate.gameObject.SetActive(true);
        }

        //Do specific action:
        panelToActivate.onActivationAction();
    }

    // NO TREATMENTS IN THIS CLASS, ONLY GUI SWAP

    // MAIN MENU:
    public void clickButtonPlay()
    {
        SetActivePage(PanelChooseLevelMenu);
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

    //MULTIPLE MENU:
    public void clickButtonExit()
    {
        ActivePanel.onClickAction("EXIT");
    }

    //EVENTS
    public void eventEndGame()
    {
        SetActivePage(PanelEndGameMenu);
    }
}
