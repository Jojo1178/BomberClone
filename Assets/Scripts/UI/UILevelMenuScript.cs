using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UILevelMenuScript : UIPanel
{

    public GameObject mapManager;
    
    //Composants de l'UI à mettre à jour:
    public Text mapNameUI;
    public Text textureNameUI;
    public Image mapPreviewUI;
    public Image textureFloorPreviewUI;
    public Image textureDestructibleWallPreviewUI;
    public Image textureIndestructibleWallPreviewUI;
    public Toggle mapToggleUI;
    public Toggle textureToggleUI;

    //Composants de l'UI avec des infos à récupérer:
    public InputField iaNumberUI;

    //Dictionaires de textures et cartes:
    private Dictionary<int, string> mapNameDictionary = new Dictionary<int, string>();
    private Dictionary<int, TexturePack> textureDictionary = new Dictionary<int, TexturePack>();

    //Entrée des prefabs:
    public GameObject[] floors;
    public GameObject[] destructibleWalls;
    public GameObject[] indestructibleWalls;

    private int numberOfMapsDetected = 0;
    private int numberOfTexturesDetected = 0;
    private int selectedMap = 0;
    private int selectedTexture = 0;
    private bool useRandomMap = false;
    private bool useRandomTexture = false;

    // Use this for initialization
    void Start ()
    {
        loadMaps();
        loadTextures();
        updateMapAndTexturePreview();
    }
	
	// Update is called once per frame
	void Update ()
    {
        
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

    private void loadTextures()
    {
        for (int i = 0; i < floors.Length; i++)
        {
            TexturePack texturePack = new TexturePack("PackTexture"+i, floors[i], destructibleWalls[i], indestructibleWalls[i]);
            textureDictionary.Add(i, texturePack);
            numberOfTexturesDetected++;
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
            Texture2D SpriteTexture = LoadTexture("Assets/Maps/" + mapNameWithoutExt + ".png");
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

    private void updateTextures()
    {
        //On récupère le texture pack selectionné:
        TexturePack selectedTexturePack = textureDictionary[selectedTexture];
        
        //On affiche le nom du pack texture selectionné dans l'UI:
        textureNameUI.text = selectedTexturePack.getTextureName();

        //On récupère la preview des prefabs du texture pack sélectionné:
        Texture2D floorPreview = getAssetPreview(selectedTexturePack.getFloorPrefab());
        Texture2D destructibleWallPreview = getAssetPreview(selectedTexturePack.getDestructibleWallPrefab());
        Texture2D indestructibleWallPreview = getAssetPreview(selectedTexturePack.getIndestructibleWallPrefab());
        
        //On affiche la preview des prefabs du texture pack selectionné:
        Sprite floorSprite = new Sprite();
        Sprite destructibleWallSprite = new Sprite();
        Sprite indestructibleWallSprite = new Sprite();
        
        floorSprite = Sprite.Create(floorPreview, new Rect(0, 0, floorPreview.width, floorPreview.height), new Vector2(0, 0), 100.0f);
        textureFloorPreviewUI.sprite = floorSprite;

        destructibleWallSprite = Sprite.Create(destructibleWallPreview, new Rect(0, 0, destructibleWallPreview.width, destructibleWallPreview.height), new Vector2(0, 0), 100.0f);
        textureDestructibleWallPreviewUI.sprite = destructibleWallSprite;

        indestructibleWallSprite = Sprite.Create(indestructibleWallPreview, new Rect(0, 0, indestructibleWallPreview.width, indestructibleWallPreview.height), new Vector2(0, 0), 100.0f);
        textureIndestructibleWallPreviewUI.sprite = indestructibleWallSprite;
    }
    
    public void clickButtonPlay()
    {
        //On récupère la Map choisie à envoyer au MapGenerator:
        string choosenMapFilePath = chooseMap();

        //On récupère le TexturePack choisi à envoyer au MapGenerator:
        TexturePack choosenTexturePack = chooseTexture();

        //On récupère le MapGenerator:
        MapGenerator mapGenerator = mapManager.GetComponent<MapGenerator>();

        //On récupère le nombre de joueurs IA:
        mapGenerator.setIANumber(chooseIANumber());

        //On envoie les informations au MapGenerator:
        mapGenerator.launchLevel(choosenMapFilePath, choosenTexturePack);
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

    private TexturePack chooseTexture()
    {
        //If we want the selected map:
        int textureNumber = selectedTexture;

        //If we want a random map:
        if (useRandomTexture)
        {
            textureNumber = UnityEngine.Random.Range(0, textureDictionary.Count);
        }
        
        return textureDictionary[textureNumber];
    }

    private int chooseIANumber()
    {
        int iaNumber = 0;
        string stringFromUI = iaNumberUI.text;

        if (stringFromUI.Equals(""))
            return 0;

        try
        {
            iaNumber = int.Parse(stringFromUI);
            if (iaNumber > 4)
                iaNumber = 4;
        }
        catch (Exception ex)
        {
            Debug.LogError("Wrong value written in UI "+ex);
        }
        return iaNumber;
    }

    public void clickButtonGoRightMap()
    {
        if (selectedMap < numberOfMapsDetected - 1)
        {
            selectedMap++;
        }
        else
        {
            selectedMap = 0;
        }
        updateMapAndTexturePreview();
    }

    public void clickButtonGoLeftMap()
    {
        if (selectedMap > 0)
        {
            selectedMap--;
        }
        else
        {
            selectedMap = numberOfMapsDetected - 1;
        }
        updateMapAndTexturePreview();
    }

    public void clickButtonGoRightTexture()
    {
        if (selectedTexture < numberOfTexturesDetected - 1)
        {
            selectedTexture++;
        }
        else
        {
            selectedTexture = 0;
        }
        updateMapAndTexturePreview();
    }

    public void clickButtonGoLeftTexture()
    {
        if (selectedTexture > 0)
        {
            selectedTexture--;
        }
        else
        {
            selectedTexture = numberOfTexturesDetected - 1;
        }
        updateMapAndTexturePreview();
    }

    public void toggleRandomMap(bool value)
    {
        useRandomMap = mapToggleUI.isOn;
    }

    public void toggleRandomTexture(bool value)
    {
        useRandomTexture = textureToggleUI.isOn;
    }

    private void updateMapAndTexturePreview()
    {
        updateMaps();
        updateTextures();
    }

    private Texture2D LoadTexture(string FilePath)
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

    // GetAssetPreview is really running asynchronously in the background, and if the preview you want is not available when you ask for it you will get a null instead of a texture.
    private Texture2D getAssetPreview(GameObject objectToPreview)
    {
        int counter = 0;
        Texture2D previewOfTheObject = null;

        while (previewOfTheObject == null && counter < 75)
        {
            previewOfTheObject = AssetPreview.GetAssetPreview(objectToPreview);
            counter++;
            System.Threading.Thread.Sleep(15);
        }

        return previewOfTheObject;
    }

    public override void onActivationAction()
    {
        
    }

    public override void onClickAction(string buttonName)
    {
        
    }
}
