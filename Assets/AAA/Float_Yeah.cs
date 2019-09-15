using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float_Yeah : MonoBehaviour {
	public float dist = .25f;
	float displacement;
	public bool vibrateOnSamelevel = false;
	// Use this for initialization
	void Start () {
		displacement = vibrateOnSamelevel ? 0 : Random.Range (-100, 100);
	}
	
	// Update is called once per frame
	void Update () {
		transform.localPosition += Vector3.up * Mathf.Sin (Time.time * 1f +displacement) * Time.deltaTime * dist;
	}
}
