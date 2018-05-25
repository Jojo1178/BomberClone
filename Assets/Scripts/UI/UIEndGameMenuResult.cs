using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEndGameMenuResult : MonoBehaviour {

    private bool victory = true;
    public Text resultUI;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

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
}
