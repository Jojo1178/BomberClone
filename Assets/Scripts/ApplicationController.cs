using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationController : MonoBehaviour {

    public Camera camera;
    public MapGenerator MapGenerator;
    public VictoryManager VictoryManager;
    
	// Use this for initialization
	void Start ()
    {
        Debug.Log("Application Started");
        MapGenerator.setVictoryManager(VictoryManager);
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}
}
