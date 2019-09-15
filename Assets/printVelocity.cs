using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class printVelocity : MonoBehaviour {
    Rigidbody r;
	// Use this for initialization
	void Start () {
        r = GetComponent<Rigidbody>();
	}
	void OnCollisionEnter(Collision c)
    {

    }
	// Update is called once per frame
	void Update () {
       
	}
}
