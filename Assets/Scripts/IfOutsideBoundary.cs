using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfOutsideBoundary : MonoBehaviour {
    public GameObject player;
    public Vector3 spawnPoint;

   
        
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Vector3.Distance(transform.position, player.transform.position) > 3000f)
        {
            print("AHH");
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            player.transform.position = spawnPoint;



        }


    }
}
