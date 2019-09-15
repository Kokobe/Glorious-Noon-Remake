using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookatThis : MonoBehaviour {
	public Transform obj;
	Vector3 offset;
	// Use this for initialization
	void Start () {
		offset = transform.position -obj.position;

	}
	
	// Update is called once per frame
	void Update () {
		var targetRotation = Quaternion.LookRotation(obj.position - transform.position);
		//targetRotation = Quaternion.Euler (new Vector3 (0,targetRotation.eulerAngles.y + 180f,0));
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation,  Time.deltaTime);
		transform.position = Vector3.Slerp (transform.position, obj.position + offset, Time.deltaTime * 3f);
	}
}
