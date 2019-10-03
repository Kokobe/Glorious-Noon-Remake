using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionDie : MonoBehaviour {



    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag.Equals("Ground"))
        {
            Destroy(gameObject);
        }

    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
