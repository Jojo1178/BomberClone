using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainMenu : UIPanel
{
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
