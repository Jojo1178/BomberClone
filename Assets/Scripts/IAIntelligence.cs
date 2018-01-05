using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAIntelligence : MonoBehaviour {

    public static IAIntelligence INSTANCE;

    enum State {OFFENSIVE, DEFENSIVE};

    public GameObject mapManager;

    private int[,] map;
    private int IANumber;
    private float xPlayerA;
    private float yPlayerA;
    private float xPlayerB;
    private float yPlayerB;
    private float xPlayerC;
    private float yPlayerC;
    private float xPlayerD;
    private float yPlayerD;

    private void Awake()
    {
        INSTANCE = this;
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //On récupère les infomations sur les positions des joueurs dans le MapGenerator:
        MapGenerator mapGenerator = mapManager.GetComponent<MapGenerator>();
        map = mapGenerator.getLoadedMap();
        IANumber = mapGenerator.getIANumber();

        //On met à jour la position des différents joueurs:
        GameObject playerA = mapGenerator.getPlayerA();
        GameObject playerB = mapGenerator.getPlayerB();
        GameObject playerC = mapGenerator.getPlayerC();
        GameObject playerD = mapGenerator.getPlayerD();


        Rigidbody2D rbA = playerA.GetComponent<Rigidbody2D>();
        xPlayerA = rbA.position.x;
        yPlayerA = rbA.position.y;

        Debug.Log("Player A is located in: " + (int)xPlayerA + "," + (int)yPlayerA);

        if (IANumber >= 1)
        {
            Rigidbody2D rbB = playerB.GetComponent<Rigidbody2D>();
            xPlayerB = rbB.position.x;
            yPlayerB = rbB.position.y;

            Debug.Log("Player B is located in: " + (int)xPlayerB + "," + (int)yPlayerB);
        }

        if (IANumber >= 2)
        {
            Rigidbody2D rbC = playerC.GetComponent<Rigidbody2D>();
            xPlayerC = rbC.position.x;
            yPlayerC = rbC.position.y;

            Debug.Log("Player C is located in: " + (int)xPlayerC + "," + (int)yPlayerC);
        }

        if (IANumber >= 3)
        {
            Rigidbody2D rbD = playerD.GetComponent<Rigidbody2D>();
            xPlayerD = rbD.position.x;
            yPlayerD = rbD.position.y;

            Debug.Log("Player D is located in: " + (int)xPlayerD + "," + (int)yPlayerD);
        }
    }

    public void calculateIA()
    {
        calculateStrategy();
        calculateMovement();
        calculateAttack();
    }

    private void calculateStrategy()
    {

    }

    private void calculateMovement()
    {

    }

    private void calculateAttack()
    {

    }
}
