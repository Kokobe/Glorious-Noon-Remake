using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class printcollided : MonoBehaviour {

	// Use this for initialization
	void OnCollisionEnter (Collision c) {
        print(c.gameObject.name);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
