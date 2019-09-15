using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeCenterOfMass : MonoBehaviour {
	public Transform com;
	public Rigidbody rb;
	void Start() {
		rb = GetComponent<Rigidbody>();
		rb.centerOfMass = com.position;
	}
}
