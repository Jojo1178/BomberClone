﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    //Prefabs used to create the map:
    private GameObject floor;
    private GameObject destructibleWall;
    private GameObject indestructibleWall;
    
    private int mapSize = 10;

    // Use this for initialization
    void Start ()
    {
        Debug.Log("MAP GENERATOR");

        //Chose the texture pack:
        //chooseBiome(0);

        //Choose the map:
        //chooseMap(0);

        //Load the choosen Map:
        //int[,] loadedMap = Load(mapPath);

        //Instanciate the choosen Map with the choosen texture pack:
        //instanciateMap(loadedMap);

        //Create a basic map:
        //createMap();
    }

    // Update is called once per frame
    void Update ()
    {
        
    }
    
    public void launchLevel(string mapFilePath, TexturePack texturePack)
    {
        instanciateTextures(texturePack);
        createMap(mapFilePath);
    }

    private void instanciateTextures(TexturePack texture)
    {
        //Instanciate all textures from the choosen texture pack:
        floor = texture.getFloorPrefab();
        destructibleWall = texture.getDestructibleWallPrefab();
        indestructibleWall = texture.getIndestructibleWallPrefab();
    }
    
    private void createMap(string mapFilePath)
    {
        //We load the choosen map from its text file and instanciate it:
        int[,] loadedMap = Load(mapFilePath);
        instanciateMap(loadedMap);
    }

    private int[,] Load(string fileName)
    {
        //Declare a StreamReader:
        StreamReader reader = new StreamReader(fileName);
        string line;
        int lineNumber = 0;
        
        //Instanciate a map:
        int[,] map = new int[mapSize, mapSize];

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

    //Use the map to add all prefabs to the game:
    //1 -> Floor
    //2 -> Destructive Wall
    //3 -> Non Destructive Wall
    private void instanciateMap(int[,] map)
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

    private void createMap()
    {
        int[,] newMap = new int[mapSize, mapSize];

        createFloor(newMap);
        createBorders(newMap);
        instanciateMap(newMap);
    }

    private void createFloor(int[,] map)
    {
        for (int x = 0; x < mapSize; x++)
        {
            for (int y = 0; y < mapSize; y++)
            {
                map[x, y] = 1;
            }
        }
    }

    private void createBorders(int[,] map)
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
            map[mapSize - 1, y] = 3;
        }
    }
}
