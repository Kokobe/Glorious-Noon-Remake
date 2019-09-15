using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElephantNinja : Ninja {

	// Use this for initialization
	void Start () {
        
	}

    public override bool isGrounded()
    {
        return Physics.Raycast(transform.position + 5f*Vector3.up, Vector3.down, distToGround + .2f);
    }
    // Update is called once per frame
    void Update () {
		
	}
}
