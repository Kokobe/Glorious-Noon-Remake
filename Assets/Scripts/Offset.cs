using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Offset : MonoBehaviour {

    public Transform offset;
    public GameObject particlePrefab;
    public Transform magicPos;
    public bool isColliding = false;
    public bool isBoomerangEnchanted = false;
    public bool isChargedEnchanted = false;
    public bool isJumpEnchanted = false;
    public bool isHammerThrowEnchanted = false;
    public bool isFireBreather = false;

    void OnCollisionEnter(Collision c)
    {

        isColliding = true;
    }

    void OnCollisionExit(Collision c)
    {

        isColliding = false;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    
}
