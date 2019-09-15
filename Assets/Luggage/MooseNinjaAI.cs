using UnityEngine;
using System.Collections;

public class MooseNinjaAI : MonoBehaviour {

    public GameObject HeroHead;
    public GameObject orbSpawner;

    
    // Use this for initialization

    Rigidbody r;
    public bool isGrounded;
   public bool inRange = false;
    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag.Equals("Ground") || c.gameObject.tag.Equals("Ninja"))
        {

            isGrounded = true;
           
        }
       


    }

    void OnCollisionExit(Collision c)
    {
        if (c.gameObject.tag.Equals("Ground") || c.gameObject.tag.Equals("Ninja"))
        {

            isGrounded = false;
        }

       
    }



    IEnumerator MoveMoose()
    {
        r = GetComponent<Rigidbody>();

        float distance = (HeroHead.transform.position - transform.position).magnitude;
        float stopDist = Random.Range(5.7f, 11f);
        float jumpHeight = 8f;
        float jumplength = Random.Range(3f, 4.2f);
        float pastTime = Time.time;

        while (distance > stopDist) //If you still have distance to go and you are in the air,
        {
            
            if (isGrounded || Time.time - pastTime > 10f)
            {
                 print(Time.time - pastTime > 10f);
                r.velocity = Vector3.up * jumpHeight + (HeroHead.transform.position - transform.position).normalized * jumplength;
                pastTime = Time.time;
               

            }
            
          
            
            yield return new WaitForSeconds(3f);
            distance = (HeroHead.transform.position - transform.position).magnitude;
        }

        inRange = true;

        pt = Time.time;


    }
    public float pt; //time marker of being in range of player
   
	// Update is called once per frame
	void Update () {

        
        float difference;
        
        if (inRange)
        {
            
            difference = Time.time - pt;
            if (difference > 4f) { //start counting

                isGrounded = true;
            }
        }


        if (inRange && isGrounded)
        {
            orbSpawner.SetActive(true);
           // orbSpawner.GetComponent<OrbSpawner>().player = HeroHead;


        }



    }
}
