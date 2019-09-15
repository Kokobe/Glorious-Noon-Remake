using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Game {
    public static Game current;
    // Use this for initialization
    public GameObject swordsSaved;

    public Game(GameObject s)
    {
        swordsSaved = s;
    }
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
