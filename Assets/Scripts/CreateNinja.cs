using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateNinja : MonoBehaviour {
    public Transform InstatiationPt;
    public GameObject[] enemies;
    public Transform target;
    void OnCollisionEnter(Collision c)
    {
        if (c.collider.attachedRigidbody.gameObject.tag.Equals("Player"))
        {
            var go = Instantiate(enemies[Random.Range(0, enemies.Length)], InstatiationPt.position, Quaternion.identity);
            go.GetComponent<Ninja>().targett = target;

        }
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
