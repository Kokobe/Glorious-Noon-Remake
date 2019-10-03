using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnableUnParent : MonoBehaviour {

    void OnEnable()
    {
        transform.parent = null;
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
