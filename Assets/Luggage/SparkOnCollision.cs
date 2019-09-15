using UnityEngine;
using System.Collections;

public class SparkOnCollision : MonoBehaviour {
    public GameObject sparks;
    public bool reliesOnHammerThrow = false;
    public bool affectsHealth = true;
    public bool explosion = false;
    public float power = 680f;
    public float powerOnPlayer = 680f;
    public float velocityNeeded = 0f;
    public bool onlyAffectedBySword = false;
    Rigidbody r;
    public bool baseOnImpulse = false;
    public float waitTime = 0f;
    float t = 0;
    public string boomColliderName = "";
    public GameObject cam;
    void Start()
    {
        r = GetComponent<Rigidbody>();

    }
	// Use this for initialization
	void OnCollisionEnter(Collision c)
    {

        if (GameObject.Find("Main Camera (eye)") != null)
            cam = (GameObject.Find("[CameraRig]"));

        var velo = r? r.velocity.magnitude : 0;

        if (reliesOnHammerThrow)
            velo += Vector3.Distance(transform.position, cam.transform.position) - 4f;

        if (r != null && (velo > velocityNeeded ||(baseOnImpulse && c.impulse.magnitude > velocityNeeded)) && t > waitTime)
        { 
            if (boomColliderName.Equals(""))
            {
                
                GameObject go = (GameObject)Instantiate(sparks, c.contacts[0].point, transform.rotation);
                Destroy(go, 5f);
                t = 0f;

              

            }
            else if (boomColliderName.Length != 0)
            {
                if (c.contacts[0].thisCollider.gameObject.name.Equals(boomColliderName) || (GetComponent<HammerThrow>() && reliesOnHammerThrow))
                {
                    if (explosion)
                    {
                        Vector3 explosionPos = c.contacts[0].point;
                        Collider[] colliders = Physics.OverlapSphere(explosionPos, 4f);
                        var num = 0;

                        foreach (Collider hit in colliders)
                        {
                            Rigidbody rb = hit.GetComponent<Rigidbody>();
                            // Physics.IgnoreCollision(hit, c.gameObject.GetComponent<Collider>());
                            //Destroy(hit.gameObject, 20f);
                            if (hit.transform.parent && hit.transform.parent.gameObject.tag.Equals("Ninja") && reliesOnHammerThrow)
                                hit.transform.parent.gameObject.GetComponent<Ninja>().hits += 40;
                            if (rb != null)
                            {
                                num++;

                                rb.AddExplosionForce(power, explosionPos, 4f, 3.0F);
                                
                                if (hit.tag.Equals("Rock"))
                                {
                                    hit.gameObject.GetComponent<Breakable_Object>().Explode(explosionPos, power , 0f);
                                }

                               

                            }
                            else if (hit.transform.parent != null && hit.transform.parent.gameObject.GetComponent<Rigidbody>())
                            {
                                rb = hit.transform.parent.gameObject.GetComponent<Rigidbody>();

                              if(rb.gameObject != cam)
                                rb.AddExplosionForce(power * rb.mass / hit.transform.parent.childCount, explosionPos, 10f, 3.0F);
                            }


                            if (hit.gameObject.name.Equals("[VRTK][AUTOGEN][BodyColliderContainer]") || hit.gameObject.name.Equals("[VRTK][AUTOGEN][FootColliderContainer]") && powerOnPlayer != 0f)
                            {
                                if(affectsHealth)
                                    cam.GetComponent<onHeadCollision>().gl.t -= .2f;
                      
                                num++;
                                rb = cam.GetComponent<Rigidbody>();
                                //rb.velocity = Vector3.zero;
                               // if(powerOnPlayer == 0f && )

                                rb.AddExplosionForce(50f *powerOnPlayer, explosionPos, 10f, 3.0F);

                                // print(Cam.name);
                            }


                        }

                        print("Colliders hit: " + num);


                    }
                    if(onlyAffectedBySword && c.gameObject.tag.Equals("Sword"))
                    {
                        GameObject go = (GameObject)Instantiate(sparks, c.contacts[0].point, Quaternion.identity);
                        Destroy(go, 10f);
                        t = 0f;
                    }else if (!onlyAffectedBySword)
                    {
                        GameObject go = (GameObject)Instantiate(sparks, c.contacts[0].point, Quaternion.identity);
                        Destroy(go, 10f);
                        t = 0f;
                    }
                   
                }


            }   
           

        }







    }
	
	// Update is called once per frame
	void Update () {
        t += Time.deltaTime;
	}
}
