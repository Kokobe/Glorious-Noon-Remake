using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class totheGround : MonoBehaviour {
    public float distToGround;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        print("elephant: " + Physics.Raycast(transform.position + 3f * Vector3.up, -Vector3.up, distToGround + .3f));
       
	}
}
