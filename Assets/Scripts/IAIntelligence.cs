﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    
    private bool mapInitialized = false;

    private void Awake()
    {
        INSTANCE = this;
    }

    // ===== MAP VALUES =====
    //1 -> Floor
    //2 -> Destructive Wall
    //3 -> Non Destructive Wall
    //4 -> Spawn player A
    //5 -> Spawn player B
    //6 -> Spawn player C
    //7 -> Spawn player D
    //8 -> Bombs
    //9 -> Flames
    // ===== ===== ===== =====

    // Use this for initialization
    void Start ()
    {
        
    }

    public void initializeMap(int [,] map)
    {
        Debug.Log("MAP RECEIVED BY IAIntelligence");
        this.map = map;
        removeSpawnValuesFromMap();

        mapInitialized = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (mapInitialized)
        {
            //On récupère les infomations sur les positions des joueurs dans le MapGenerator:
            MapGenerator mapGenerator = mapManager.GetComponent<MapGenerator>();
            IANumber = mapGenerator.getIANumber();

            //On met à jour la position des différents joueurs:
            GameObject playerA = mapGenerator.getPlayerA();
            GameObject playerB = mapGenerator.getPlayerB();
            GameObject playerC = mapGenerator.getPlayerC();
            GameObject playerD = mapGenerator.getPlayerD();

            if (playerA != null)
            {
                Rigidbody2D rbA = playerA.GetComponent<Rigidbody2D>();
                xPlayerA = rbA.position.x;
                yPlayerA = rbA.position.y;

                //Debug.Log("Player A is located in: " + (int)xPlayerA + "," + (int)yPlayerA);
            }


            if (IANumber >= 1 && playerB != null)
            {
                Rigidbody2D rbB = playerB.GetComponent<Rigidbody2D>();
                xPlayerB = rbB.position.x;
                yPlayerB = rbB.position.y;

                //Debug.Log("Player B is located in: " + (int)xPlayerB + "," + (int)yPlayerB);
            }

            if (IANumber >= 2 && playerC != null)
            {
                Rigidbody2D rbC = playerC.GetComponent<Rigidbody2D>();
                xPlayerC = rbC.position.x;
                yPlayerC = rbC.position.y;

                //Debug.Log("Player C is located in: " + (int)xPlayerC + "," + (int)yPlayerC);
            }

            if (IANumber >= 3 && playerD != null)
            {
                Rigidbody2D rbD = playerD.GetComponent<Rigidbody2D>();
                xPlayerD = rbD.position.x;
                yPlayerD = rbD.position.y;

                //Debug.Log("Player D is located in: " + (int)xPlayerD + "," + (int)yPlayerD);
            }
        }        
    }

    public void addBombToMap(float x, float y)
    {
        this.writeMapInLogFile(this.map, "BEFORE BOMB TO BE PLANTED");
        map[(int)x, (int)y] = 8;
        this.writeMapInLogFile(this.map, "BOMB PLANTED");
    }
    
    public void addFlameToMap(float x, float y)
    {
        this.writeMapInLogFile(this.map, "BEFORE FLAME TO BE ADDED");
        map[(int)x, (int)y] = 9;
        this.writeMapInLogFile(this.map, "FLAME ADDED");
    }

    //Called when bombs, flames and destroyed walls are removed from the map:
    //string parameter is only used for logging purpose.
    public void removeEntityFromMap(float x, float y, string entity)
    {
        this.writeMapInLogFile(this.map, "BEFORE "+entity+" TO BE REMOVED");
        map[(int)x, (int)y] = 1;
        this.writeMapInLogFile(this.map, entity+" REMOVED");
    }

    //Les emplacements des spawns des joueurs ne sont pas utilisés par l'IA, on les remplace par la valeur 1 
    //qui correspond à un Floor afin de faciliter les traitements:
    private void removeSpawnValuesFromMap()
    {
        Debug.Log("REMOVING SPAWN VALUES FROM MAP");
        this.writeMapInLogFile(this.map, "BEFORE REMOVING SPAWN VALUES");

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

        Debug.Log("SPAWN VALUES REMOVED FROM MAP");
        this.writeMapInLogFile(this.map, "SPAWN VALUES REMOVED");
    }

    // DEBUG FUNCTION:
    private void writeMapInLogFile(int [,] map, string logInfo)
    {
        try
        {

            //Pass the filepath and filename to the StreamWriter Constructor
            StreamWriter sw = new StreamWriter("D:\\UnityProjects\\BomberClone\\Assets\\Logs\\log.txt", true);

            sw.WriteLine("===================");
            sw.WriteLine(logInfo);
            sw.WriteLine("===================");

            //Write the map representation inthe log file:
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    //Debug.Log(map[x, y] + " ");
                    sw.Write(map[x, y] + " ");
                }
                sw.WriteLine("");
            }

            //Close the file
            sw.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }
    }

    //Retourne true si la position est sur une case bloqué (block/bomb/perso)
    private bool isBlockedPosition(Vector2 iaPosition)
    {
        //TODO
        throw new NotImplementedException();
    }
    

    //Retourne true si la position est sur une case dangeureuse (bomb/flame)
    private bool isDangerousPosition(Vector2 iaPosition)
    {
        //TODO
        throw new NotImplementedException();
    }

    public bool getObjective(Vector2 iaPosition, ref Vector2 iaObjective)
    {
        if (isDangerousPosition(iaPosition))
        {
            getOffensiveGoal(iaPosition, ref iaObjective);
            return true;
        }
        else
        {
            getDefensiveGoal(iaPosition, ref iaObjective);
            return false;
        }
    }

    private void getOffensiveGoal(Vector2 iaPosition, ref Vector2 iaObjective)
    {
        int nx = UnityEngine.Random.Range(0, 2);
        int ny = UnityEngine.Random.Range(0, 2);
        int[] dir = { -1, 0, 1 };

        Vector2 newPos = iaPosition + new Vector2(dir[nx], dir[ny]);
        if (!isDangerousPosition(newPos) && isBlockedPosition(newPos))
        {
            iaObjective = newPos;
        }
    }

    private void getDefensiveGoal(Vector2 iaPosition, ref Vector2 iaObjective)
    {
        List<Vector2> path = getClosestSafePositionPath(iaPosition);
        if(path.Count > 1)
        {
            iaObjective = path[1];
        }
    }


    private List<Vector2> getClosestSafePositionPath(Vector2 initialPosition)
    {
        Queue<Vector2> todo = new Queue<Vector2>();
        List<Vector2> done = new List<Vector2>();
        Dictionary<Vector2, List<Vector2>> paths = new Dictionary<Vector2, List<Vector2>>();

        todo.Enqueue(initialPosition);
        paths.Add(initialPosition, new List<Vector2>() { initialPosition });
        Vector2[] nextPos = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

        while(todo.Count > 0)
        {
            Vector2 current = todo.Dequeue();
            if (!isDangerousPosition(current)) return paths[current];

            foreach (Vector2 dir in nextPos)
            {
                Vector2 newdir = current + dir;
                if (done.Contains(newdir)) continue;
                if (!todo.Contains(newdir))
                {
                    if (!isBlockedPosition(newdir))
                    {
                        List<Vector2> path = paths[current];
                        path.Add(newdir);
                        paths.Add(newdir, path);
                        todo.Enqueue(newdir);
                    }
                    else
                    {
                        done.Add(newdir);
                    }
                }
                done.Add(current);
            }
        }
        return null;
    }

}
