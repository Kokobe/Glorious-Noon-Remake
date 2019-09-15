using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootWeapon : MonoBehaviour {

    public Transform player;
    public GameObject prefab;
    public float height;
    public float timeUntilHit;
	// Use this for initialization
	void Start () {
        StartCoroutine(ThrowStuff());
	}

    IEnumerator ThrowStuff()
    {
        while (true)
        {
            var go =  Instantiate(prefab, transform.position, Quaternion.identity);
            Vector3 v = new Vector3();
            if ((player.position - transform.position).magnitude > 50f)
            {
                timeUntilHit = 3f;

            }
            else
                timeUntilHit = 1f;
          //  go.GetComponent<Rigidbody>().useGravity = false;
            v.x = (player.position.x - transform.position.x) / timeUntilHit;
            v.z = (player.position.z - transform.position.z) / timeUntilHit;
            v.y = ((player.position.y - transform.position.y) + .5f * Physics.gravity.magnitude *timeUntilHit * timeUntilHit) / timeUntilHit;
        //    print((player.position.y - transform.position.y));
            go.GetComponent<Rigidbody>().velocity = v;
            Destroy(go, 15f);
            yield return new WaitForSecondsRealtime(3f);
        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
