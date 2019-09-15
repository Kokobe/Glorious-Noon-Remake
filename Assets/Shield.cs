using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {
    public Vector3 offset = new Vector3(0f, -0.84f,.5f);
    public Transform playerHead;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += Vector3.Distance(transform.position, playerHead.position) * (playerHead.position + offset  - transform.position) * Time.deltaTime ;
        transform.rotation = Quaternion.Euler(0, playerHead.rotation.eulerAngles.y, 0);
	}
}
