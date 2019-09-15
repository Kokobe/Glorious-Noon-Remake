using UnityEngine;
using System.Collections;

public class SparkOnCollisionForChildren : MonoBehaviour
{
    //attatch to parent rigidbody object.
    public GameObject sparks;
    public float velocityNeeded = 0f;
    Rigidbody r;
    void Start()
    {
        r = GetComponent<Rigidbody>();

    }
    // Use this for initialization
    void OnCollisionEnter(Collision c)
    {
        if (r != null && r.velocity.magnitude > velocityNeeded)
        {
            GameObject go = (GameObject)Instantiate(sparks, c.contacts[0].point, Quaternion.identity);
            Destroy(go, 5f);

        }
        else
        {


        }





    }

    // Update is called once per frame
    void Update()
    {

    }
}
