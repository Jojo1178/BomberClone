using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TexturePack
{
    private string textureName;
    private GameObject floorPrefab;
    private GameObject destructibleWallPrefab;
    private GameObject indestructibleWallPrefab;

    public TexturePack(string textureName, GameObject floorPrefab, GameObject destructibleWallPrefab, GameObject indestructibleWallPrefab)
    {
        this.textureName = textureName;
        this.floorPrefab = floorPrefab;
        this.destructibleWallPrefab = destructibleWallPrefab;
        this.indestructibleWallPrefab = indestructibleWallPrefab;
    }
	
    public string getTextureName()
    {
        return this.textureName;
    }

    public GameObject getFloorPrefab()
    {
        if (floorPrefab == null)
            Debug.Log("<color=red>ACHTUNG !</color> Floor Prefab null for texture pack "+textureName);
        return this.floorPrefab;
    }

    public GameObject getDestructibleWallPrefab()
    {
        if (destructibleWallPrefab == null)
            Debug.Log("<color=red>ACHTUNG !</color> DestructibleWall Prefab null for texture pack " + textureName);
        return this.destructibleWallPrefab;
    }

    public GameObject getIndestructibleWallPrefab()
    {
        if (indestructibleWallPrefab == null)
            Debug.Log("<color=red>ACHTUNG !</color> IndestructibleWall Prefab null for texture pack " + textureName);
        return this.indestructibleWallPrefab;
    }
}
