using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onParticleCollision : MonoBehaviour {

    void OnParticleCollision(GameObject other)
    {
        Rigidbody body = other.GetComponent<Rigidbody>();
        if (body)
        {
            Vector3 direction = other.transform.position - transform.position;
            direction = direction.normalized;
            body.AddForce(direction * 5);
        }
        if (other.transform.parent && other.transform.parent.gameObject.GetComponent<Ninja>())
        {
            Ninja n = other.transform.parent.gameObject.GetComponent<Ninja>();
            n.hits += 30;
            Vector3 direction = other.transform.position - transform.position;
            direction = direction.normalized;
           
            n.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(direction * -1000f * n.gameObject.GetComponent<Rigidbody>().mass, other.transform.position);
            if (other.GetComponent<Collider>().name.Equals(n.weakSpotName))
            {
                n.hits += n.weakSpotDamage;
                other.GetComponent<Breakable_Object>().Explode(other.transform.position, 300f, 0f);

            }

        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
