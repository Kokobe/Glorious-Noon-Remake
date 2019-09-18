using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Ninja: MonoBehaviour {
    //add moves here
    public bool bigNinja = false;
    public float stunTime = 1f;
    public bool Elephant = false;
    public bool SpawnNinja = false;
    public bool MooseNinja = false;
    public bool stupid = false;
    public bool shooter = false;
    public int weakSpotDamage = 60;
    public float resistanceTime = 1f;
    public bool dontChase = false;
    public bool idle = false;
    public bool spinTumble = true;
    public bool charge = false;
    public bool justSpin = false;
    public bool smash = false;
    public bool deathKnightSmash = true;
    public string weakSpotName = "";
    public Transform[] spawnPoints;
    public GameObject spawnedObject;
    public int health = 3;
    public float hitDrag = 2f;
    public int hits = 0;
    public float defense = 1f;
    public float angDrag = 8f;
	public float turnSpeed = 3f;
	public float angleCutOff = 8f;
	public float groundedVelocity = 2f;
	public float MaxVelocity = 10f;
	public float pauseTime = .8f;
	public float speedLimit;
	public float speedLimitForce;
    public float chargeRange = 8f;
    public float playerRange = 50f;
    Coroutine spawnS;
	//public Material eyes;
	public bool backwards = true;
	Color originalColor;
	Rigidbody r;
	public Transform forward;
	Vector3 forwardDir;

	public Quaternion toRotation;
	public Transform targett;
	public bool gettingBackUp;
	public bool waiting;
	public int interruptted = 0;
	Coroutine f = null;
	public float distToGround;
	public Collider c;
    bool choosingRotation = false;
    public Image progressBar;
    public float amount;
	// Use this for initialization

    IEnumerator checkIfOnMap()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(30f);
            if (Vector3.Distance(targett.position, transform.position) > 1000f)
            {
                StopCoroutine(checkIfOnMap());
                Destroy(gameObject);

            }
                
        }
    }

	void Start () {
		//originalColor = eyes.color;
		r = GetComponent<Rigidbody> ();
        
		//r.constraints = RigidbodyConstraints.FreezeRotation;
		forwardDir = (forward.position - transform.position).normalized;
		toRotation = transform.rotation;
		distToGround = !Elephant ? c.bounds.extents.y : 4.39f;
        if(GameObject.Find("Main Camera (eye)") != null && !stupid)
            targett = GameObject.Find("Main Camera (eye)").transform;

      ///  StartCoroutine(checkIfOnMap());

    }


	public virtual bool isGrounded(){
		return Elephant ? Physics.Raycast(transform.position + 3f* Vector3.up, -Vector3.up, distToGround + .3f) : Physics.Raycast (transform.position, -Vector3.up, distToGround +.3f);

	}
    void onCollisionExit(Collision c)
    {
        if (c.gameObject.tag.Equals("Sword"))
        {
            
        }

    }

	void OnCollisionEnter(Collision c){
		

		 if (c.gameObject.tag.Equals ("Sword")) {

            if (c.gameObject.transform.childCount != 0 && c.gameObject.transform.GetChild(0).name.Equals("Lightning"))
            {
                GetComponent<Breakable_Object>().enabled = true;
                if (progressBar)
                    progressBar.fillAmount += amount;
            }
               
            else
                hits+= 1 + (int) (c.rigidbody.velocity.magnitude/defense);
           
            if (c.contacts[0].thisCollider.name.Equals(weakSpotName))
            {
                if (SpawnNinja)
                {
                    hits += 100;
                    var cc = c.contacts[0].thisCollider.gameObject;
                    cc.GetComponent<Breakable_Object>().Explode(cc.transform.position, 1000f, 0f);
                }else if (Elephant)
                    hits += 50;
                else if(!SpawnNinja && spawnPoints.Length != 0)
                {
                    hits += weakSpotDamage;
                    var cc = c.contacts[0].thisCollider.gameObject;
                    cc.GetComponent<Breakable_Object>().Explode(cc.transform.position, 0, 0f);
                }else
                {
                    hits += weakSpotDamage;
                    var cc = c.contacts[0].thisCollider.gameObject;
                    cc.GetComponent<Breakable_Object>().Explode(cc.transform.position, 300f, 0f);
                }

            }
                
            r.useGravity = true;
            
            r.drag = hitDrag;
           
           r.angularDrag = .3f;
            r.constraints = RigidbodyConstraints.None;
			if (!gettingBackUp)
				f = StartCoroutine (FixRotation ());
			if (gettingBackUp) {
				//interruptted++;
				StopCoroutine (f);
				gettingBackUp = false;
				//eyes.color = originalColor;
				f = StartCoroutine (FixRotation ());

			}


		} 
			

	}

	bool actuallyGrounded(){
		return r.velocity.magnitude < groundedVelocity && isGrounded();

	}

    bool velocityCloseToZero()
    {
        return r.velocity.magnitude < .01f;
    }

    IEnumerator chargeAtPlayer()
    {
        if(shooter)
            r.velocity += Vector3.up * 15f;
        r.velocity += Vector3.up * 20f;
        r.useGravity = false;
        r.drag = 3f;
        var sparky = GetComponent<SparkOnCollision>();

        if (sparky.boomColliderName != null)
        {
            sparky.enabled = true;
        }
        yield return new WaitForSeconds(1.1f);
        r.mass = 300f;
        float tt = 0f;
        Quaternion from = transform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(targett.position+ Vector3.down *3f - transform.position);
        if (backwards)
            targetRotation = Quaternion.Euler(new Vector3(targetRotation.eulerAngles.x- (Random.Range(0f,1f) < .3 ? 0: 180f), targetRotation.eulerAngles.y + 180f, targetRotation.eulerAngles.z));
      //  else
            //targetRotation = Quaternion.Euler(new Vector3(0, targetRotation.eulerAngles.y, 0));
        while (tt <= 1f)
        {

            transform.rotation = Quaternion.Lerp(from, targetRotation, tt);


            tt += Time.fixedDeltaTime * 2f;



            yield return null;

        }

        r.drag = 0;
        r.velocity += (targett.position + Vector3.down*1.8f * (shooter? 0: 1) - transform.position).normalized * 25f;
       // r.angularVelocity = Vector3.right * 100000f;
        yield return new WaitForSeconds(3f);

        r.useGravity = true;
        r.mass = 1f;
        if (sparky.boomColliderName != null)
        {
            sparky.enabled = false;
        }

    }

    IEnumerator SpawnStuff()
    {
        r.velocity += Vector3.up * (shooter ? 5f:20f);
        if (shooter)
            r.velocity += Vector3.right * 15f;
        r.useGravity = false;
        r.drag = 4f;
        r.angularDrag = angDrag;
        yield return new WaitForSeconds(1.1f);
        if (!SpawnNinja)
        {
            float tt = 0f;
            Quaternion from = transform.rotation;
            Quaternion targetRotation = Quaternion.LookRotation(targett.position + Vector3.down * 3f - transform.position);
            if (backwards)
                targetRotation = Quaternion.Euler(new Vector3(0, targetRotation.eulerAngles.y + 180f, 0));
            else
                targetRotation = Quaternion.Euler(new Vector3(0, targetRotation.eulerAngles.y, 0));

            while (tt <= 1f)
            {

                transform.rotation = Quaternion.Lerp(from, targetRotation, tt);


                tt += Time.fixedDeltaTime * 2f;



                yield return null;

            }

        }
        yield return new WaitForSeconds(1f);
        foreach (Transform tran in spawnPoints)
        {
            var tarRot = Quaternion.LookRotation(targett.position - tran.position);
            var go = Instantiate(spawnedObject, tran.position,tarRot);
            
            var nin = go.GetComponent<Ninja>();

            if (nin)
            {
                nin.targett = targett;
                if (nin.backwards)
                    go.transform.rotation = Quaternion.Euler(new Vector3(tarRot.eulerAngles.x, tarRot.eulerAngles.y + 180f, tarRot.eulerAngles.z));
                else
                    go.transform.rotation = Quaternion.Euler(new Vector3(tarRot.eulerAngles.x, tarRot.eulerAngles.y, tarRot.eulerAngles.z));

            }

            if (!SpawnNinja)
                yield return new WaitForSeconds(.5f);

            Vector3 offset = SpawnNinja ? Vector3.down * 1.8f : Vector3.zero;

            go.GetComponent<Rigidbody>().velocity += (targett.position + offset - tran.position).normalized * 15f;
            Destroy(go, 15f);
        }
        yield return new WaitForSeconds(1);
        r.drag = 0;
        r.angularDrag = 0f;
        r.useGravity = true;
        if (SpawnNinja)
        {
          
            r.angularDrag = 8f;
        }
        spawnS = null;

    }

    void Moves()
    {
                 if(Vector3.Distance(transform.position, targett.position) < chargeRange)
                {
                if(!SpawnNinja && !MooseNinja)
            {
                Debug.Log("death smashing");
                idle = false;
                r.drag = 0;
                r.angularDrag = 0;
                if (deathKnightSmash)
                {
                    var gSpark = GetComponent<SparkOnCollision>();
                    if (gSpark != null)
                        gSpark.enabled = true;

                    r.angularDrag = 0f;
                    r.maxAngularVelocity = 5.5f;
                    r.AddForceAtPosition(Vector3.down * 10000f, forward.position);
                    r.velocity = Vector3.zero;
                    r.velocity += forwardDir * 1f;
                    r.velocity += Vector3.up * 75f;
                 


                }
                else if (spinTumble)
                {
                    r.velocity += forwardDir * 8f;
                    r.velocity += Vector3.up * 4f;

                    r.angularVelocity = new Vector3(1000f, 1000f, 0);
                   

                }
                else if (justSpin)
                {
                    r.velocity += forwardDir * 8f;
                    r.velocity += Vector3.up * 4f;
                    r.maxAngularVelocity = 30f;
                    r.angularVelocity = new Vector3(0, 1000f, 0);

                }
                else if (smash)
                {
                    r.drag = 0;
                    r.angularDrag = 0;
                    r.maxAngularVelocity = 30f;
                    r.AddForceAtPosition(Vector3.down * 10000f, forward.position);

                    r.velocity = Vector3.zero;
                    r.velocity += Vector3.Normalize(targett.position - transform.position) * Mathf.Sqrt(2 * 9.81f * Vector3.Distance(transform.position, targett.position) / 3f) / 3f;
                    r.velocity += Vector3.up * 45f;
                    f = StartCoroutine(FixRotation());
                    gettingBackUp = true;

                }
                else if (charge || shooter)
                {
                    if(charge && shooter)
                    {
                        if(Random.Range(0f, 1f) < .55f)
                        {
                            StartCoroutine(chargeAtPlayer());
                        }else
                            StartCoroutine(SpawnStuff());
                    }
                    else if (charge)
                         StartCoroutine(chargeAtPlayer());
                    else if (shooter)
                        StartCoroutine(SpawnStuff());



                }

               
            

            }else
            {
               // r.drag = hitDrag;
               if(spawnS == null)
                 spawnS = StartCoroutine(SpawnStuff());


            }

            r.constraints = RigidbodyConstraints.None;
            if (!gettingBackUp && !bigNinja && !MooseNinja)
                f = StartCoroutine(FixRotation());


        }
                else if(Vector3.Distance(transform.position, targett.position) < playerRange)
                {
            if (!dontChase) { 
                    idle = false;
                if(!bigNinja)
                    r.drag = 0;
                    r.constraints = RigidbodyConstraints.None;
                if (Random.Range(0f, 1f) > .1f)
                {

                    r.velocity += forwardDir * 5f;
                    r.velocity += Vector3.up * Random.Range(4f, 8f);
                    r.constraints = RigidbodyConstraints.None;
                    r.angularDrag = angDrag;




                }
                else
                {
                    if (deathKnightSmash)
                    {
                        var gSpark = GetComponent<SparkOnCollision>();
                        if (gSpark != null)
                            gSpark.enabled = true;

                        Debug.Log("death smashing");
                        if(!bigNinja)
                        r.angularDrag = 0;
                        r.maxAngularVelocity = 10f;
                        r.AddForceAtPosition(Vector3.down * 10000f, forward.position);
                        r.velocity = Vector3.zero;
                        r.velocity += forwardDir * 1f;
                        r.velocity += Vector3.up * 45f;
                        if (!gettingBackUp && !bigNinja && !MooseNinja)
                            f = StartCoroutine(FixRotation());

                    }
                    else if (!Elephant)
                    {
                        if(!bigNinja)
                             r.angularDrag = 0;
                      //  r.maxAngularVelocity = 30f;
                        //r.AddForceAtPosition(Vector3.down * 10000f, forward.position);

                        r.velocity = Vector3.zero;
                        r.velocity += forwardDir * 2.5f;
                        r.velocity += Vector3.up * 5f ;
                        if (!gettingBackUp && !bigNinja && !MooseNinja)
                            f = StartCoroutine(FixRotation());

                    }




                }

                      
                    }


                }else  ///on idle
                {
                    idle = true;
                    r.drag = hitDrag;
                    r.constraints = RigidbodyConstraints.None;
                    float rand = Random.Range(0f, 1f);
                   // print(rand);
                    if(rand < .4f)
                    {
                        r.angularDrag = 0;
                        r.maxAngularVelocity = 10f;
                        r.velocity += Vector3.up * 8f;
                       // r.angularVelocity += new Vector3(0, 90f, 0);

                    }
                    else if (rand < .5f)
                    {
                        r.angularDrag = 0;
                        r.maxAngularVelocity = 10f;
                        r.AddForceAtPosition(Vector3.down * 10000f, forward.position);
                        r.velocity = Vector3.zero;
                        r.velocity += forwardDir * 1f;
                        r.velocity += Vector3.up * 45f;
                        if (!gettingBackUp)
                            f = StartCoroutine(FixRotation());

                    }
                    else if (rand <.75f)
                    {
                        r.angularDrag = 0;
                        r.velocity += Vector3.up * 8f;
                        r.angularVelocity += new Vector3(0, -90f, 0);
                    }
                    else
                    {
                        r.angularDrag = 0;
                        r.maxAngularVelocity = 10f;
                        r.velocity += Vector3.up * 8f;
                        r.angularVelocity += new Vector3(0, 90f, 0);
                       
                    }

                   

                }
				

    }

	 
	IEnumerator FixRotation(){
      
       
		float t = 0f;
		gettingBackUp = true;
		//eyes.color = Color.red;
		waiting = true;
        yield return new WaitForSeconds(resistanceTime);

        r.drag = 0f;
        r.angularDrag = .3f;

        yield return new WaitForSeconds(stunTime);
        
        if (Elephant)
            r.drag = 0f;
        else if (!bigNinja)
            r.drag = 0f;
        yield return new WaitUntil(actuallyGrounded);
        r.drag = hitDrag;
        r.angularDrag = angDrag;
        if (deathKnightSmash)
        {
           var u = GetComponent<SparkOnCollision>();
            if (u)
                u.enabled = false;


        }
        waiting = false;
		Quaternion from = transform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(targett.position - transform.position);
        if (idle && !choosingRotation)
        {
            StartCoroutine(chooseRandRotation());

        }else if(!idle)
        {

            if (backwards)
                toRotation = Quaternion.Euler(new Vector3(0, targetRotation.eulerAngles.y + 180f, 0));
            else
                toRotation = Quaternion.Euler(new Vector3(0, targetRotation.eulerAngles.y, 0));

        }
       
      
       //yield return new WaitUntil(actuallyGrounded);
        r.drag = hitDrag;
        while (t <= 1f) {
			
				transform.rotation = Quaternion.Lerp (from, toRotation, t);


			if (r.velocity.magnitude < groundedVelocity) {
				

			}



				t += Time.fixedDeltaTime;



			yield return null;

		}
        //eyes.color = originalColor;
        //r.constraints = RigidbodyConstraints.FreezeRotation;
        r.angularDrag = 0;
		gettingBackUp = false;
        r.drag = 0f;
		yield return null;

	}



	float t;
	public bool counting = false;
	// Update is called once per frame


    IEnumerator chooseRandRotation()
    {
        choosingRotation = true;

        yield return new WaitForSeconds(3);
        Quaternion targetRotation = Quaternion.LookRotation(Random.onUnitSphere);

        if (backwards)
            toRotation = Quaternion.Euler(new Vector3(0, targetRotation.eulerAngles.y + 180f, 0));
        else
            toRotation = Quaternion.Euler(new Vector3(0, targetRotation.eulerAngles.y, 0));

       

        choosingRotation = false;

    }

	void Update () {

        try
        {
            if (hits > health)
            {

                //  GetComponent<Breakable_Object>().enabled = true;
                GetComponent<Breakable_Object>().Explode(transform.position, 600f, 0f);
                if (progressBar)
                    progressBar.fillAmount += amount;
            }


            if (r.velocity.magnitude > speedLimit)
            {

                r.AddForce(-r.velocity.normalized * speedLimitForce);

            }

            bool grounded = isGrounded();

            forwardDir = (transform.position != null) ? (forward.position - transform.position).normalized : Vector3.zero;
            Quaternion targetRotation = Quaternion.LookRotation(targett.position - transform.position);
            if (idle && !choosingRotation)
            {
                StartCoroutine(chooseRandRotation());



            }
            else if (!idle)
            {

                if (backwards)
                    toRotation = Quaternion.Euler(new Vector3(0, targetRotation.eulerAngles.y + 180f, 0));
                else
                    toRotation = Quaternion.Euler(new Vector3(0, targetRotation.eulerAngles.y, 0));

            }
            targetRotation = toRotation;
            if (grounded && !counting)
            {
                t = Time.time;
                counting = true;
            }
            else if (!grounded)
            {
                counting = false;

            }


            if (r.velocity.magnitude < groundedVelocity && !gettingBackUp && Quaternion.Angle(targetRotation, transform.rotation) < angleCutOff)
            {

                if (counting && Time.time - t > pauseTime)
                {

                    Moves();

                    counting = false;
                    t = 0;
                }

            }


            if (!gettingBackUp && !bigNinja && grounded && turnSpeed != 0)
            {


                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);



            }

            if (!gettingBackUp && grounded && !turning && bigNinja)
            {
                turning = true;
                turningT = 0;
                fr = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z));

            }
            if (turning && !gettingBackUp && bigNinja)
            {


                transform.rotation = Quaternion.Lerp(fr, targetRotation, turningT);
                turningT += Time.deltaTime * turnSpeed;

                print(transform.rotation == fr);
            }



            if (Quaternion.Angle(targetRotation, transform.rotation) < angleCutOff)
            {
                turning = false;
            }
        } catch (UnassignedReferenceException e)
        {
            Debug.Log("Ninja does not have target");
        }



    }

    public bool turning = false;
    Quaternion fr;
    
    float turningT = 0f;
}
