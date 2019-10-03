using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickToGround : MonoBehaviour {
    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag.Equals("Sword") && c.rigidbody.velocity.magnitude > 3.7f && c.gameObject.GetComponent<FixedJoint>() == null && !c.gameObject.GetComponent<Boomerang>() && !c.gameObject.GetComponent<Slippery>())
        {
            c.gameObject.AddComponent<FixedJoint>();
            c.gameObject.AddComponent<stuck>();
            c.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            c.gameObject.transform.parent = null;
            var Rphys = c.gameObject.GetComponent<RFX4_PhysXSetImpulse>();
            if (Rphys)
            {
                Destroy(Rphys);
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
