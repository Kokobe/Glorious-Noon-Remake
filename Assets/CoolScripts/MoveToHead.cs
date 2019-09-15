using UnityEngine;
using System.Collections;

public class MoveToHead : MonoBehaviour {
    public GameObject head;
    public bool shifted = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (shifted) transform.position = head.transform.position + Vector3.down * .4f;
        else
            transform.position = head.transform.position + Vector3.up * 2f;

    }
}
