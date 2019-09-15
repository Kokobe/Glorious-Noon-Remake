using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable_Object : MonoBehaviour {
    public float velocityNeeded = 0f;
    public bool spontaneouslyExplode = false;
    public bool rock = true;
    public bool partOfNinja = false;
    public bool shield = false;
    public bool important = true;
    //public bool chainRxn = false;
    public GameObject chainObj;
    public GameObject original;
    public GameObject[] toDestroy;
    public GameObject fragments;
    public MeshCollider rockCollider;
    public MeshRenderer m;
    public GameObject Cam;
    Rigidbody r;
    public float radius;
    public float power = 680f;
    public float powerOnPlayer = 680f;

    IEnumerator slowDownTime()
    {
        Time.timeScale = .1f;
        yield return new WaitForSecondsRealtime(3f);
        Time.timeScale = .25f;
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 1f;



    }

   public void Explode(Vector3 positionExplosion, float strength, float strengthOnPlayer) {
        Vector3 initVelocity = r ? r.velocity : Vector3.zero;
        if (important)
            StartCoroutine(slowDownTime());

        GameObject[] g = new GameObject[transform.childCount];
        if (!rock)
        {
            gameObject.GetComponent<Ninja>().enabled = false;
            for (int i = 0; i < transform.childCount; i++)
            {
                GameObject child = transform.GetChild(i).gameObject;
                if (child.GetComponent<Collider>() != null)
                {
                    var childR = child.AddComponent<Rigidbody>();
                    childR.useGravity = true;

                    childR.velocity += initVelocity/2;

                    g[i] = child;


                }
                else
                    g[i] = child;

            }

            for (int i = 0; i < g.Length; i++)
            {
                var go = g[i];

                go.transform.parent = null;
                Destroy(go, 20f);

                for (int j = 0; j < fragments.transform.childCount; j++)
                {
                    if (go.GetComponent<Collider>() != null )
                        Physics.IgnoreCollision(go.GetComponent<Collider>(), fragments.transform.GetChild(j).gameObject.GetComponent<Collider>());


                }


            }
        }


        if(rockCollider)
            rockCollider.enabled = false;
        m.enabled = false;
        foreach (GameObject ggo in toDestroy)
        {
            Destroy(ggo);
        }

        fragments.SetActive(true);
        fragments.transform.parent = null;

        Vector3 explosionPos = positionExplosion;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);

        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            // Physics.IgnoreCollision(hit, c.gameObject.GetComponent<Collider>());
            //Destroy(hit.gameObject, 20f);
            if (rb != null)
            {
                if (!rock)
                    rb.velocity += initVelocity;
                rb.AddExplosionForce(strength, explosionPos, radius, 2.8F);
            }
            if (hit.gameObject.name.Equals("[VRTK][AUTOGEN][BodyColliderContainer]") && rock)
            {
                rb = Cam.GetComponent<Rigidbody>();
                //rb.velocity = Vector3.zero;
                rb.AddExplosionForce(50f * strengthOnPlayer, explosionPos, radius, 3.0F);

                // print(Cam.name);
            }

        }

        Destroy(fragments, 10f);
        if (rock && !shield && !partOfNinja)
            Destroy(transform.parent.gameObject, 10f);
        else
        {

            Destroy(gameObject, 10f);
        }
    }

    void OnCollisionEnter(Collision c)
    {
        Vector3 v = c.impulse;
 
        Vector3 initVelocity = r ? r.velocity : Vector3.zero;
        if (shield)
        {
            if (c.gameObject.tag.Equals("Ninja") && GetComponent<Breakable_Object>().enabled)
            {
                StartCoroutine(slowDownTime());
                rockCollider.enabled = false;
                m.enabled = false;

                fragments.SetActive(true);
                fragments.transform.parent = null;

                Vector3 explosionPos = c.contacts[0].point;
                Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);

                foreach (Collider hit in colliders)
                {
                    Rigidbody rb = hit.GetComponent<Rigidbody>();
                    // Physics.IgnoreCollision(hit, c.gameObject.GetComponent<Collider>());
                    //Destroy(hit.gameObject, 20f);
                    if (rb != null)
                    {
                       
                            rb.velocity += initVelocity;
                        rb.AddExplosionForce(power, explosionPos, radius, 1.0F);
                    }
                   

                }

                Destroy(fragments, 10f);
                Destroy(gameObject, 5f);


            }



        }
        else
        {
            if (c.gameObject.tag.Equals("Sword") && GetComponent<Breakable_Object>().enabled && c.relativeVelocity.magnitude > velocityNeeded)
            {

                if (important)
                    StartCoroutine(slowDownTime());

                GameObject[] g = new GameObject[transform.childCount];
                if (!rock)
                {
                    gameObject.GetComponent<Ninja>().enabled = false;
                    foreach (GameObject ggo in toDestroy)
                    {
                        Destroy(ggo);
                    }

                    if (toDestroy.Length == 0)
                    {
                        for (int i = 0; i < transform.childCount; i++)
                        {
                            GameObject child = transform.GetChild(i).gameObject;
                            if (child.GetComponent<Collider>() != null)
                            {
                                var childR = child.AddComponent<Rigidbody>();
                                childR.useGravity = true;

                                childR.velocity += initVelocity;

                                g[i] = child;


                            }
                            else
                                g[i] = child;

                        }
                    }
                    else
                        g = new GameObject[0];
                   

                    for (int i = 0; i < g.Length; i++)
                    {
                        var go = g[i];

                        go.transform.parent = null;
                        Destroy(go, 20f);

                        for (int j = 0; j < fragments.transform.childCount; j++)
                        {
                            if (go.GetComponent<Collider>() != null)
                                Physics.IgnoreCollision(go.GetComponent<Collider>(), fragments.transform.GetChild(j).gameObject.GetComponent<Collider>());


                        }


                    }
                }


                if(rockCollider)
                    rockCollider.enabled = false;
                if(m)
                    m.enabled = false;
               

                fragments.SetActive(true);
                fragments.transform.parent = null;

                Vector3 explosionPos = c.contacts[0].point;
                Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);

                foreach (Collider hit in colliders)
                {
                    Rigidbody rb = hit.GetComponent<Rigidbody>();
                    // Physics.IgnoreCollision(hit, c.gameObject.GetComponent<Collider>());
                    //Destroy(hit.gameObject, 20f);
                    if (rb != null)
                    {
                        if (!rock)
                            rb.velocity += initVelocity;
                        rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
                    }
                    if (hit.gameObject.name.Equals("[VRTK][AUTOGEN][BodyColliderContainer]") && rock && Cam != null)
                    {
                        rb = Cam.GetComponent<Rigidbody>();
                        //rb.velocity = Vector3.zero;
                        rb.AddExplosionForce(50f * powerOnPlayer, explosionPos, radius, 3.0F);

                        // print(Cam.name);
                    }

                }

                Destroy(fragments, 10f);
                if (rock && !partOfNinja)
                {
                    Destroy(transform.parent.gameObject);
                    print("LETS GO");
                }
                else
                {

                    Destroy(gameObject, 10f);
                }

                if (chainObj)
                {
                    chainObj.GetComponent<Breakable_Object>().Explode(transform.position, 100f, 0);
                }

            }



        }



    }

  public void setCam(GameObject c)
    {
        Cam = c;
    }

	// Use this for initialization
	void Start() {
        r = GetComponent<Rigidbody>();
        if (rock)
        {
            rockCollider = GetComponent<MeshCollider>();

            m = GetComponent<MeshRenderer>();
        }
        else
            gameObject.GetComponent<Breakable_Object>().enabled = false;
        if (GameObject.Find("Main Camera (eye)") != null)
            setCam(GameObject.Find("Main Camera (eye)").transform.parent.gameObject);


    }
	
	// Update is called once per frame
	void Update () {
        /*
        if (spontaneouslyExplode)
        {
            Explode(transform.position, 100f, 0);
            print("FUCK");
        }
          */ 
	}
}
