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
        return this.floorPrefab;
    }

    public GameObject getDestructibleWallPrefab()
    {
        return this.destructibleWallPrefab;
    }

    public GameObject getIndestructibleWallPrefab()
    {
        return this.indestructibleWallPrefab;
    }
}
