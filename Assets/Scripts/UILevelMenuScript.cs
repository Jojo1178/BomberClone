using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class UILevelMenuScript : MonoBehaviour {

    public Text mapNameUI;
    public Toggle mapToggleUI;
    public Toggle textureToggleUI;

    private Dictionary<int, string> mapNameDictionary = new Dictionary<int, string>();
    private Dictionary<int, string> mapPreviewDictionary = new Dictionary<int, string>();

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
        updateRandom();
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
            //else if (file.Extension == ".jpg" || file.Extension == ".png")
            //{
            //   mapPreviewDictionary.Add(mapCounter, file.Name);
            //}
        }
    }

    private void updateMaps()
    {
        //On affiche le nom de la map selectionnée dans l'UI:
        mapNameUI.text = mapNameDictionary[selectedMap];

        //On affiche la preview de la map selectionée dans l'UI:
    }

    private void updateRandom()
    {
        
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
    }
}
