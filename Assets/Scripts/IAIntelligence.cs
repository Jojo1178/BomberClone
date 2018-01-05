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

    private bool mapCleaned = false;

    private void Awake()
    {
        INSTANCE = this;
    }

    //1 -> Floor
    //2 -> Destructive Wall
    //3 -> Non Destructive Wall
    //4 -> Spawn player A
    //5 -> Spawn player B
    //6 -> Spawn player C
    //7 -> Spawn player D
    //8 -> Bombs
    //9 -> Flames

    // Use this for initialization
    void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        //On récupère les infomations sur les positions des joueurs dans le MapGenerator:
        MapGenerator mapGenerator = mapManager.GetComponent<MapGenerator>();
        IANumber = mapGenerator.getIANumber();

        //On récupère la carte une seule fois, pas la peine de la récupérer à chaque appel de l'Update:
        if (map == null)
        {
            map = mapGenerator.getLoadedMap();
        }
        else if (!mapCleaned)
        {
            //Si on a récupéré la map mais qu'elle n'a pas été encore nettoyée, on le fait:
            removeSpawnValuesFromMap();
        }

        //On met à jour la position des différents joueurs:
        GameObject playerA = mapGenerator.getPlayerA();
        GameObject playerB = mapGenerator.getPlayerB();
        GameObject playerC = mapGenerator.getPlayerC();
        GameObject playerD = mapGenerator.getPlayerD();


        Rigidbody2D rbA = playerA.GetComponent<Rigidbody2D>();
        xPlayerA = rbA.position.x;
        yPlayerA = rbA.position.y;
        
        //Debug.Log("Player A is located in: " + (int)xPlayerA + "," + (int)yPlayerA);

        if (IANumber >= 1)
        {
            Rigidbody2D rbB = playerB.GetComponent<Rigidbody2D>();
            xPlayerB = rbB.position.x;
            yPlayerB = rbB.position.y;

            //Debug.Log("Player B is located in: " + (int)xPlayerB + "," + (int)yPlayerB);
        }

        if (IANumber >= 2)
        {
            Rigidbody2D rbC = playerC.GetComponent<Rigidbody2D>();
            xPlayerC = rbC.position.x;
            yPlayerC = rbC.position.y;

            //Debug.Log("Player C is located in: " + (int)xPlayerC + "," + (int)yPlayerC);
        }

        if (IANumber >= 3)
        {
            Rigidbody2D rbD = playerD.GetComponent<Rigidbody2D>();
            xPlayerD = rbD.position.x;
            yPlayerD = rbD.position.y;

            //Debug.Log("Player D is located in: " + (int)xPlayerD + "," + (int)yPlayerD);
        }
    }

    public void addBombToMap(float x, float y)
    {
        map[(int)x, (int)y] = 8;
    }

    public void addFlameToMap(float x, float y)
    {
        map[(int)x, (int)y] = 9;
    }

    public void removeBombFromMap(float x, float y)
    {
        map[(int)x, (int)y] = 1;
    }

    public void removeFlameFromMap(float x, float y)
    {
        map[(int)x, (int)y] = 1;
    }

    //Les emplacements des spawns des joueurs ne sont pas utilisés par l'IA, on les remplace par la valeur 1 
    //qui correspond à un Floor afin de faciliter les traitements:
    private void removeSpawnValuesFromMap()
    {
        Debug.Log("BEFORE REMOVING SPAWN VALUES");
        //this.drawASCIImap(this.map);

        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                int tileType = map[x, y];

                if (tileType == 4 || tileType == 5 || tileType  == 6 || tileType == 7)
                {
                    map[x, y] = 1;
                }
            }
        }
        mapCleaned = true;

        Debug.Log("AFTER REMOVING SPAWN VALUES");
        //this.drawASCIImap(this.map);
    }

    // DEBUG FUNCTION:
    private void drawASCIImap(int[,] map)
    {
        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                Debug.Log(map[x, y]+" ");
            }
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
