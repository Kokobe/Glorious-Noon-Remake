using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class specialStickToGround : MonoBehaviour {
    public OnStep a;
    bool alreadyDidIt = false;
    public SwordCount swc;
    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag.Equals("Sword") && c.rigidbody.velocity.magnitude > 2f && c.gameObject.GetComponent<FixedJoint>() == null && (c.gameObject.GetComponent<Boomerang>() && !alreadyDidIt)  && !c.gameObject.GetComponent<Slippery>())
        {
            c.gameObject.AddComponent<FixedJoint>();
            Destroy(c.gameObject.GetComponent<MeshCollider>());
           // c.gameObject.AddComponent<stuck>();
            c.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            var Rphys = c.gameObject.GetComponent<RFX4_PhysXSetImpulse>();
            if (Rphys)
            {
                Destroy(Rphys);
            }
            StartCoroutine(a.Actions());
            alreadyDidIt = true;
            swc.attSwords.Clear();
            swc.svdSwords.Clear();
            Destroy(c.gameObject, 10f);
            Destroy(GetComponent<specialStickToGround>(), 11f);
        }

    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
