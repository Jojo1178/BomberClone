using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        SetActivePage();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private GameObject ActivePanel;
    public GameObject PanelMainMenu;
    public GameObject PanelPauseMenu;

    private void SetActivePage()
    {
        //PanelMainMenu.SetActive(false);
        //PanelPauseMenu.SetActive(true);
    }

    public void clickButtonPlay()
    {
        Debug.Log("BUTTON PLAY CLICKED");
    }

    public void clickButtonExit()
    {
        Debug.Log("BUTTON EXIT CLICKED");
    }

    public void clickButtonResume()
    {
        Debug.Log("BUTTON RESUME CLICKED");
    }

    public void clickButtonOptions()
    {
        Debug.Log("BUTTON OPTIONS CLICKED");
    }
}
