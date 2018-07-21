using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIIngameMenu : UIPanel
{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void clickButtonPause()
    {
        Time.timeScale = 0;
    }

    public override void onActivationAction()
    {
        
    }

    public override void onClickAction(string buttonName)
    {
        
    }
}
