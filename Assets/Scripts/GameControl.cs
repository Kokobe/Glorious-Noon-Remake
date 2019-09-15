using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameControl : MonoBehaviour {
    public static GameControl control;
    public GameObject SavedSwords;

	// Use this for initialization
	void Awake () {
        if(control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
		    
        else if(control != this)
        {
            Destroy(gameObject); 
        }
	}

    public void Save(GameObject g)
    {
        SavedSwords = g;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        Game data = new Game(g);
        bf.Serialize(file, data);
        file.Close();
        print(Application.persistentDataPath + "/playerInfo.dat");
    }

    public void Load()
    {
        if(File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            Game data = (Game)bf.Deserialize(file);
            file.Close();
            if(data.swordsSaved != null)
                SavedSwords = data.swordsSaved;


        }
    }
	
	
}
