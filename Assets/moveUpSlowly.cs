using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveUpSlowly : MonoBehaviour {
    private Vector3 realPosition;
    private Vector3 oldPosition;
	// Use this for initialization
	void OnEnable () {
        realPosition = transform.position;
        transform.position -= Vector3.up * 10f;
        oldPosition = transform.position;
        
	}
    float journey = 0;
    void move()
    {
        journey += Time.deltaTime;
          transform.position = Vector3.Lerp(oldPosition, realPosition, journey);
        
    }
	// Update is called once per frame
	void Update () {
        move();
    }
}
