using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    //Players prefabs:
    public GameObject refPlayerA;
    private GameObject instPlayerA;

    public GameObject refPlayerB;
    private GameObject instPlayerB;

    public GameObject refPlayerC;
    private GameObject instPlayerC;

    public GameObject refPlayerD;
    private GameObject instPlayerD;

    //Prefabs used to create the map:
    private GameObject floor;
    private GameObject destructibleWall;
    private GameObject indestructibleWall;
    
    public int mapSize = 10;
    private int IANumber = 0;

    private int[,] loadedMap;
    private List<GameObject> tilesObjects = new List<GameObject>();

    private VictoryManager victoryManager;

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
        //Instancie les textures à utiliser:
        instanciateTextures(texturePack);

        instanciatePlayers();

        //Crée la carte:
        createMap(mapFilePath);
    }

    private void placePlayers()
    {

    }

    private void instanciateTextures(TexturePack texture)
    {
        //Instanciate all textures from the choosen texture pack:
        floor = texture.getFloorPrefab();
        destructibleWall = texture.getDestructibleWallPrefab();
        indestructibleWall = texture.getIndestructibleWallPrefab();
    }

    private void instanciatePlayers()
    {
        instPlayerA = refPlayerA;
        instPlayerB = refPlayerB;
        instPlayerC = refPlayerC;
        instPlayerD = refPlayerD;
    }

    private void createMap(string mapFilePath)
    {
        //We load the choosen map from its text file and instanciate it:
        this.loadedMap = Load(mapFilePath);
        instanciateMap(loadedMap);

        //Send the map to the IAIntelligence:
        IAIntelligence.INSTANCE.initializeMap(loadedMap);
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
    //4 -> Spawn player A
    //5 -> Spawn player B
    //6 -> Spawn player C
    //7 -> Spawn player D

    private void instanciateMap(int[,] map)
    {
        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                int tileType = map[x, map.GetLength(1)-1-y];

                switch (tileType)
                {
                    case 1:
                        tilesObjects.Add(Instantiate(floor, new Vector3(x, y, 0), Quaternion.identity));
                        break;
                    case 2:
                        //For destructible positions, we instanciate a Floor tile + Destrucible prefab:
                        tilesObjects.Add(Instantiate(floor, new Vector3(x, y, 0), Quaternion.identity));
                        tilesObjects.Add(Instantiate(destructibleWall, new Vector3(x, y, 0), Quaternion.identity));
                        break;
                    case 3:
                        tilesObjects.Add(Instantiate(indestructibleWall, new Vector3(x, y, 0), Quaternion.identity));
                        break;
                    case 4:
                        //For players positions, we instanciate a Floor tile + Player prefab
                        tilesObjects.Add(Instantiate(floor, new Vector3(x, y, 0), Quaternion.identity));
                        instPlayerA = Instantiate(refPlayerA, new Vector3(x, y, 0), Quaternion.identity);
                        break;
                    case 5:
                        //For players positions, we instanciate a Floor tile + Player prefab
                        tilesObjects.Add(Instantiate(floor, new Vector3(x, y, 0), Quaternion.identity));
                        if (IANumber >= 1)
                            instPlayerB = Instantiate(refPlayerB, new Vector3(x, y, 0), Quaternion.identity);    
                        break;
                    case 6:
                        //For players positions, we instanciate a Floor tile + Player prefab
                        tilesObjects.Add(Instantiate(floor, new Vector3(x, y, 0), Quaternion.identity));
                        if (IANumber >= 2)
                            instPlayerC = Instantiate(refPlayerC, new Vector3(x, y, 0), Quaternion.identity);
                        break;
                    case 7:
                        //For players positions, we instanciate a Floor tile + Player prefab
                        tilesObjects.Add(Instantiate(floor, new Vector3(x, y, 0), Quaternion.identity));
                        if (IANumber >= 3)
                            instPlayerD = Instantiate(refPlayerD, new Vector3(x, y, 0), Quaternion.identity);
                        break;
                }
            }
        }
    }

    public void cleanScene()
    {
        Debug.Log("CLEANING SCENE...");
        destroyMap();
        destroyPlayers();
        Debug.Log("...SCENE CLEANED");
    }

    //Remove all instanciated tiles from scene:
    private void destroyMap()
    {
        for (int i = tilesObjects.Count - 1; i >= 0; i--)
        {
            GameObject tileToRemove = tilesObjects[i];
            Destroy(tileToRemove);
        }

        Debug.Log("MAP DESTROYED");
    }

    //Remove all instanciated players from scene:
    private void destroyPlayers()
    {
        Destroy(instPlayerA);
        Destroy(instPlayerB);
        Destroy(instPlayerC);
        Destroy(instPlayerD);

        Debug.Log("PLAYERS DESTROYED");
    }

    [Obsolete]
    private void createMap()
    {
        int[,] newMap = new int[mapSize, mapSize];

        createFloor(newMap);
        createBorders(newMap);
        instanciateMap(newMap);
    }

    [Obsolete]
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

    [Obsolete]
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

    public int[,] getLoadedMap()
    {
        return loadedMap;
    }

    public int getIANumber()
    {
        return IANumber;
    }

    public void setIANumber(int IANumber)
    {
        this.IANumber = IANumber;
        victoryManager.setAliveIANumber(IANumber);
    }

    public GameObject getPlayerA()
    {
        return instPlayerA;
    }

    public GameObject getPlayerB()
    {
        return instPlayerB;
    }

    public GameObject getPlayerC()
    {
        return instPlayerC;
    }

    public GameObject getPlayerD()
    {
        return instPlayerD;
    }

    public void setVictoryManager(VictoryManager victoryManager)
    {
        this.victoryManager = victoryManager;
    }
}
