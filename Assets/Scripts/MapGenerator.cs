using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    public GameObject[] floors;
    public GameObject[] destructibleWalls;
    public GameObject[] indestructibleWalls;

    private GameObject floor;
    private GameObject destructibleWall;
    private GameObject indestructibleWall;

    private int mapSize = 10;
    private bool randomBiome = true;

    // Use this for initialization
    void Start ()
    {
        Debug.Log("MAP GENERATOR");

        chooseBiome();
        createMap();
    }

    // Update is called once per frame
    void Update ()
    {
        
    }

    private void createMap()
    {
        //1 -> Floor
        //2 -> Destructive Wall
        //3 -> Non Destructive Wall

        int[,] newMap = new int[mapSize, mapSize];

        createFloor(newMap);
        createBorders(newMap);
        instanciateMap(newMap);
    }

    private void chooseBiome()
    {
        //TODO Vérifier si toutes les listes ont la meme taille.
        if (randomBiome)
        {
            int biomeNumber = Random.Range(0, floors.GetLength(0)-1);

            floor = floors[biomeNumber];
            destructibleWall = destructibleWalls[biomeNumber];
            indestructibleWall = indestructibleWalls[biomeNumber];
        }
        else
        {
            //TODO
        }
    }

    private void createFloor(int [,] map)
    {
        for (int x = 0; x < mapSize; x++)
        {
            for (int y = 0; y < mapSize; y++)
            {
                map[x, y] = 1;
            }
        }
    }

    private void createBorders(int [,] map)
    {
        for (int x = 0; x < mapSize; x++) //BAS
        {
            map[x, 0] = 3;
        }

        for (int x = 0; x < mapSize; x++) //HAUT
        {
            map[x, mapSize - 1] = 3;
        }

        for (int y = 0; y < mapSize; y++) //GAUCHE
        {
            map[0, y] = 3;
        }

        for (int y = 0; y < mapSize; y++) //DROITE
        {
            map[mapSize-1, y] = 3;
        }
    }

    private void instanciateMap(int [,] map)
    {
        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                int tileType = map[x, y];

                switch (tileType)
                {
                    case 1:
                        Instantiate(floor, new Vector3(x, y, 0), Quaternion.identity);
                        break;
                    case 2:
                        Instantiate(destructibleWall, new Vector3(x, y, 0), Quaternion.identity);
                        break;
                    case 3:
                        Instantiate(indestructibleWall, new Vector3(x, y, 0), Quaternion.identity);
                        break;
                }
            }
        }
    }
}
