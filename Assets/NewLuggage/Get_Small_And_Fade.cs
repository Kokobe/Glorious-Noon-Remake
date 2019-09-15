using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Get_Small_And_Fade : MonoBehaviour {
	float t;
	Vector3 scale;
	public BoxCollider b ;
	Vector3 boxSize;
	// Use this for initialization
	void Start () {
		t = 0f;
	scale = transform.localScale;
		boxSize = b.size;
	}
	
	// Update is called once per frame
	void Update () {
		//var num = (t + 1) * (t + 1);

		Vector3 theVector = Vector3.Slerp(scale, scale*.4f, t);
	//	theVector.y = scale.y;

		transform.localScale = theVector;

		Vector3 theBoxSize = Vector3.Lerp (boxSize, boxSize * .4f, t);
		theBoxSize.z = boxSize.z; 
		b.size = theBoxSize ;

		t += Time.deltaTime/30f;
	}
}
