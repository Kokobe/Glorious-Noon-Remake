using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour {
    public float force = 14f;
	// Use this for initialization
	void Start () {
        var r = gameObject.GetComponent<Rigidbody>();
        r.useGravity = false;
    }
	
	// Update is called once per frame
	void Update () {
        var r = gameObject.GetComponent<Rigidbody>();

        if (r)
        {
           
           r.AddForce(force * Vector3.down * GetComponent<Rigidbody>().mass);
        }
	}
}
