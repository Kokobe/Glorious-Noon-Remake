using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneStats : MonoBehaviour {

    public GameObject prefab;
    public float time = 5f;
    public float baseHeight = -4.3f;
    public float range = .1f;
   
    Rigidbody r;
	// Use this for initialization
	void Start () {
        r = GetComponent<Rigidbody>();
        if(baseHeight < 0)
        {
            baseHeight = transform.position.y;
        }
	}

    float t = 0;
	// Update is called once per frame
	void Update () {
        Vector3 to = Vector3.up;
        float dist = Mathf.Abs(transform.position.y - baseHeight);
       
        if (!gameObject.GetComponent<DoMagic>())
        {
            
            transform.position = new Vector3(transform.position.x, range * Mathf.Sin((2* Mathf.PI/time) * Time.time) + baseHeight, transform.position.z);



        }
	}
}
