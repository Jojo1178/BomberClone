using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    public GameObject[] floors;
    public GameObject[] destructibleWalls;
    public GameObject[] indestructibleWalls;

    public string mapPath;
    public string[] mapList;

    private GameObject floor;
    private GameObject destructibleWall;
    private GameObject indestructibleWall;

    private int mapSize = 10;
    private bool randomBiome = false;

    // Use this for initialization
    void Start ()
    {
        Debug.Log("MAP GENERATOR");

        //Chose the texture pack used:
        chooseBiome(0);

        //Create a simple map:
        //createMap();

        //Load a Map from a file:
        int[,] loadedMap = Load(mapPath);
        instanciateMap(loadedMap);
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

    private void chooseBiome(int number)
    {
        int biomeNumber = number;
        
        if (randomBiome)
        {
            biomeNumber = UnityEngine.Random.Range(0, floors.GetLength(0)-1);
        }

        //TODO Vérifier si toutes les listes ont la meme taille.
        floor = floors[biomeNumber];
        destructibleWall = destructibleWalls[biomeNumber];
        indestructibleWall = indestructibleWalls[biomeNumber];
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

    private int[,] Load(string fileName)
    {
        //Declare a StreamReader:
        StreamReader reader = new StreamReader(fileName);
        string line;
        int lineNumber = 0;

        //Instanciate a map:
        int[,] map = new int[10,10]; //TODO: Compter le nombre de lignes et colonnes du fichier

        //Parse each line of the text file:
        while ((line = reader.ReadLine()) != null)
        {
            int columnNumber = 0;

            //For each line, take each caracter:
            foreach (char c in line)
            {
                //Convert it to int and add it into our map:
                int tileValue = (int)Char.GetNumericValue(c);
                map[columnNumber,lineNumber] = tileValue;
                columnNumber++;
            }
            lineNumber++;
        }

        //Close the StreamReader:
        reader.Close();

        return map;
    }
}
