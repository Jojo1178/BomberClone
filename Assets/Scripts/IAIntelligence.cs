using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAIntelligence : MonoBehaviour {

    enum State { OFFENSIVE, DEFENSIVE };

    public GameObject mapManager;

    private int[,] map;
    private float xPlayerA;
    private float yPlayerA;
    private float xPlayerB;
    private float yPlayerB;
    private float xPlayerC;
    private float yPlayerC;
    private float xPlayerD;
    private float yPlayerD;


    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        ////On récupère les infomations sur les positions des joueurs dans le MapGenerator:
        //MapGenerator mapGenerator = mapManager.GetComponent<MapGenerator>();
        //map = mapGenerator.getLoadedMap();

        ////On met à jour la position des différents joueurs:
        //GameObject playerA = mapGenerator.getPlayerA();
        //GameObject playerB = mapGenerator.getPlayerB();
        //GameObject playerC = mapGenerator.getPlayerC();
        //GameObject playerD = mapGenerator.getPlayerD();

        //PlayerController pcA = playerA.GetComponent<PlayerController>();
        //Rigidbody2D rbA = pcA.getRigidBody();
        //xPlayerA = rbA.position.x;
        //yPlayerA = rbA.position.y;

        //PlayerController pcB = playerB.GetComponent<PlayerController>();
        //Rigidbody2D rbB = pcB.getRigidBody();
        //xPlayerB = rbB.position.x;
        //yPlayerB = rbB.position.y;

        //PlayerController pcC = playerC.GetComponent<PlayerController>();
        //Rigidbody2D rbC = pcC.getRigidBody();
        //xPlayerC = rbC.position.x;
        //yPlayerC = rbC.position.y;

        //PlayerController pcD = playerD.GetComponent<PlayerController>();
        //Rigidbody2D rbD = pcD.getRigidBody();
        //xPlayerD = rbD.position.x;
        //yPlayerD = rbD.position.y;
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
