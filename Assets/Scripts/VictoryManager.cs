using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryManager : MonoBehaviour {

    public Canvas canvas;

    private bool playerIsAlive = true;
    private int aliveIANumber = 0;

    private MapGenerator mapGenerator;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void checkVictory()
    {
        if (aliveIANumber <= 0)
        {
            endGame(true);
        }
        else if (!playerIsAlive)
        {
            endGame(false);
        }
    }

    public void playerDied()
    {
        playerIsAlive = false;

        checkVictory();
    }

    public void iaDied()
    {
        aliveIANumber--;

        checkVictory();
    }

    private void endGame(bool victory)
    {
        UIController uiController = canvas.GetComponent<UIController>();
        uiController.eventEndGame();

        UIEndGameMenuResult uiEndGameMenuResult = canvas.GetComponentInChildren<UIEndGameMenuResult>();
        uiEndGameMenuResult.setVictory(victory);

        mapGenerator.cleanScene();
    }

    public void setAliveIANumber(int aliveIANumber)
    {
        this.aliveIANumber = aliveIANumber;
    }

    public void setMapGenerator(MapGenerator mapGenerator)
    {
        this.mapGenerator = mapGenerator;
    }
}
