using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyNinja : MonoBehaviour {

    void OnTriggerEnter(Collider c)
    {
        if (c.attachedRigidbody && c.attachedRigidbody.gameObject.tag.Equals("Ninja"))
        {
            Destroy(c.attachedRigidbody.gameObject);
        }


    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
