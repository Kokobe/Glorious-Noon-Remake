using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTextColorOnCollide : MonoBehaviour {

    TextMesh t;
    void OnCollisionEnter(Collision c)
    {
        t.color = Color.blue;
        print("Changed");



    }
	// Use this for initialization
	void Start () {
        t = GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
