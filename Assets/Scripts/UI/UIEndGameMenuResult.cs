using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEndGameMenuResult : UIPanel
{

    private bool victory = true;
    public Text resultUI;

    public void setVictory(bool victory)
    {
        this.victory = victory;

        if (victory)
        {
            resultUI.text = "VICTORY !";
        }
        else
        {
            resultUI.text = "DEFEAT !";
        }
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
