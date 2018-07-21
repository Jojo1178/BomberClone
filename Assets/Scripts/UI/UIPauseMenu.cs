﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPauseMenu : UIPanel
{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void clickButtonResume()
    {
        Time.timeScale = 1;
    }

    public override void onActivationAction()
    {
        
    }

    public override void onClickAction(string buttonName)
    {
        if (buttonName.Equals("EXIT"))
        {
            exitGame();
        }
    }

    private void exitGame()
    {
        Application.Quit();
    }
}
