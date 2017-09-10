using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class UILevelMenuScript : MonoBehaviour {

    public GameObject mapManager;

    public Text mapNameUI;
    public Image mapPreviewUI;
    public Toggle mapToggleUI;
    public Toggle textureToggleUI;

    private Dictionary<int, string> mapNameDictionary = new Dictionary<int, string>();

    private int numberOfMapsDetected = 0;
    private int selectedMap = 0;
    private bool useRandomMap = false;
    private bool useRandomTexture = false;

    // Use this for initialization
    void Start ()
    {
        loadMaps();
	}
	
	// Update is called once per frame
	void Update ()
    {
        updateMaps();
    }

    private void loadMaps()
    {
        //On récupère les noms des cartes du dossier cartes:
        DirectoryInfo dir = new DirectoryInfo("Assets/Maps/");
        FileInfo[] files = dir.GetFiles();

        foreach (FileInfo file in files)
        {
            string fileName = file.Name;
            
            //Si c'est un fichier texte, on considère que c'est une carte:
            if (file.Extension == ".txt")
            {
                mapNameDictionary.Add(numberOfMapsDetected, file.Name);
                numberOfMapsDetected++;
            }
        }
    }

    private void updateMaps()
    {
        //On enlève l'extention .txt au nom du fichier:
        string mapNameWithoutExt = mapNameDictionary[selectedMap].Substring(0, mapNameDictionary[selectedMap].Length - 4);

        //On affiche le nom de la map selectionnée dans l'UI:
        mapNameUI.text = mapNameWithoutExt;

        //On vérifie si cette carte a une preview:
        if (File.Exists("Assets/Maps/" + mapNameWithoutExt + ".jpg"))
        {
            //Si cette carte a une preview, on l'affiche dans l'UI (JPG):
            Sprite NewSprite = new Sprite();
            Texture2D SpriteTexture = LoadTexture("Assets/Maps/" + mapNameWithoutExt + ".jpg");
            NewSprite = Sprite.Create(SpriteTexture, new Rect(0, 0, SpriteTexture.width, SpriteTexture.height), new Vector2(0, 0), 100.0f);
            mapPreviewUI.sprite = NewSprite;
        }
        else if (File.Exists("Assets/Maps/" + mapNameWithoutExt + ".png"))
        {
            //Si cette carte a une preview, on l'affiche dans l'UI (PNG):
            Sprite NewSprite = new Sprite();
            Texture2D SpriteTexture = LoadTexture("Assets/Maps/" + mapNameWithoutExt + ".jpg");
            NewSprite = Sprite.Create(SpriteTexture, new Rect(0, 0, SpriteTexture.width, SpriteTexture.height), new Vector2(0, 0), 100.0f);
            mapPreviewUI.sprite = NewSprite;
        }
        else
        {
            //Si cette carte n'a pas de preview, on l'affiche la 404.jpg:
            Sprite NewSprite = new Sprite();
            Texture2D SpriteTexture = LoadTexture("Assets/Maps/404.jpg");
            NewSprite = Sprite.Create(SpriteTexture, new Rect(0, 0, SpriteTexture.width, SpriteTexture.height), new Vector2(0, 0), 100.0f);
            mapPreviewUI.sprite = NewSprite;
        }
    }

    public Texture2D LoadTexture(string FilePath)
    {

        // Load a PNG or JPG file from disk to a Texture2D
        // Returns null if load fails

        Texture2D Tex2D;
        byte[] FileData;

        if (File.Exists(FilePath))
        {
            FileData = File.ReadAllBytes(FilePath);
            Tex2D = new Texture2D(2, 2);           // Create new "empty" texture
            if (Tex2D.LoadImage(FileData))           // Load the imagedata into the texture (size is set automatically)
                return Tex2D;                 // If data = readable -> return texture
        }
        return null;                     // Return null if load failed
    }

    public void clickButtonGoRight()
    {
        if (selectedMap < numberOfMapsDetected-1)
        {
            selectedMap++;
        }
        else
        {
            selectedMap = 0;
        }
    }

    public void clickButtonGoLeft()
    {
        if (selectedMap > 0)
        {
            selectedMap--;
        }
        else
        {
            selectedMap = numberOfMapsDetected-1;
        }
    }

    public void toggleRandomMap(bool value)
    {
        useRandomMap = mapToggleUI.isOn;
    }

    public void toggleRandomTexture(bool value)
    {
        useRandomTexture = textureToggleUI.isOn;
    }

    public void clickButtonPlay()
    {
        //MAP CHOISIE:
        Debug.Log("MAP SELECTED: "+ mapNameDictionary[selectedMap]);
        //MAP ALEATOIRE:
        Debug.Log("RANDOM MAP: "+useRandomMap);
        //TEXTURE ALEATOIRE:
        Debug.Log("RANDOM TEXTURE: "+useRandomTexture);

        string choosenMapFilePath = chooseMap();

        //Launch the game with selected informations:
        MapGenerator mapGenerator = mapManager.GetComponent<MapGenerator>();
        mapGenerator.launchLevel(choosenMapFilePath, 0, useRandomTexture);
    }

    private string chooseMap()
    {
        //If we want the selected map:
        int mapNumber = selectedMap;

        //If we want a random map:
        if (useRandomMap)
        {
            mapNumber = UnityEngine.Random.Range(0, mapNameDictionary.Count);
        }
        
        //We add the path to the Maps directory to the map name:
        string choosenMapFilePath = "Assets/Maps/" + mapNameDictionary[mapNumber];

        return choosenMapFilePath;
    }
}
