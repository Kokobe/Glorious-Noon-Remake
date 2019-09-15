using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setTextWaveTo1 : MonoBehaviour {
    public TextMesh t;
	// Use this for initialization
	void OnEnable () {
        t.text = t.text.Substring(0, t.text.Length - 1) + 1;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
