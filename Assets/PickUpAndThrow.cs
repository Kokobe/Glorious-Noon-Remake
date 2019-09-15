using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpAndThrow : MonoBehaviour {
    public Rigidbody attatch;
    public Transform jumpDir;
    public GameObject jumpParticle;
    public float breakForcee;
    public SteamVR_TrackedObject trackedObj;
    public FixedJoint joint;
    public bool holding = false;
    public bool isBoomerangEnchanted = false;
    public bool isChargedEnchanted = false;
    public bool isJumpEnchanted = false;
    public bool isHammerThrowEnchanted = false;
    public List<GameObject> attatchedSwords = new List<GameObject>();
    public List<GameObject> savedSwords = new List<GameObject>();
    public Transform rig;
    public List<GameObject> colliders = new List<GameObject>();
    public GameObject swordManager;
   
    void OnTriggerExit(Collider c)
    {
        if (c.gameObject.tag.Equals("Sword") || c.gameObject.tag.Equals("EnchanterStone"))
        {
            var go = transform.gameObject;
            if (c.gameObject.GetComponent<Rigidbody>())
            {
                go = c.gameObject;

            }
            else
            {
                go = c.transform.parent.gameObject;



            }
            if ((colliders.IndexOf(go) != -1))
            {
                colliders.Remove(go);
            }
        }
       // print(colliders.Count);
    }



    public void pickUp(GameObject c)
    {
       
        
        var go = transform;
        var offRot = Vector3.up;


      
      
            print("Picking up");
            holding = true;
         
                go = c.transform;








        // c.gameObject.transform.rotation = attatch.rotation;
        //c.gameObject.transform.parent = transform;

        if (go.gameObject.GetComponent<FixedJoint>())
        {
            Destroy(go.gameObject.GetComponent<FixedJoint>());
            Destroy(go.gameObject.GetComponent<stuck>());

        }
        if (joint == null)
        {
            go.rotation = attatch.rotation;
            if (go.name.Equals("Shield"))
            {
                go.rotation = Quaternion.Euler(attatch.rotation.eulerAngles.x, attatch.rotation.eulerAngles.y + 20f, attatch.rotation.eulerAngles.z);
                var body = gameObject.transform.parent.gameObject.GetComponent<onHeadCollision>();
                body.shield = go.gameObject;
                body.gl = go.GetComponent<GlowUpAndDown>();
                
            }



            Offset o = go.GetComponent<Offset>();
            //  go.transform.rotation = Quaternion.Euler(go.rotation.eulerAngles.x + offRot.x, go.rotation.eulerAngles.y + offRot.y, go.rotation.eulerAngles.z + offRot.z);
            go.position = attatch.position + go.position - o.offset.position;

            isBoomerangEnchanted = o.isBoomerangEnchanted;
            isChargedEnchanted = o.isChargedEnchanted;
            isJumpEnchanted = o.isJumpEnchanted;
            isHammerThrowEnchanted = o.isHammerThrowEnchanted;



           if(attatchedSwords.IndexOf(go.gameObject) == -1)
            {
                attatchedSwords.Add(go.gameObject);
                if (!isBoomerangEnchanted && !isChargedEnchanted && !isJumpEnchanted && !isHammerThrowEnchanted)
                    attatchedSwords.Remove(go.gameObject);
                else
                {
                    if(savedSwords.IndexOf(go.gameObject) == -1)
                    {
                        savedSwords.Add(go.gameObject);
                    }

                }

               

            }


            int toRemovee = -1;
            int numOfBoomerangs = 0;
            for (int i = 0; i < attatchedSwords.Count; i++)
            {
                if (attatchedSwords[i].GetComponent<Boomerang>() && o.isBoomerangEnchanted)
                {
                    numOfBoomerangs++;
                }
            }

            for (int i = 0; i < attatchedSwords.Count; i++)
            {
                if (attatchedSwords[i] != go.gameObject && attatchedSwords[i].GetComponent<Boomerang>() && o.isBoomerangEnchanted && numOfBoomerangs >2)
                {
                    Destroy(attatchedSwords[i].GetComponent<Boomerang>());
                    attatchedSwords[i].GetComponent<Rigidbody>().useGravity = true;
                    toRemovee = i;
                }
            }
            if(toRemovee != -1)
                attatchedSwords.RemoveAt(toRemovee);
            go.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            joint = go.gameObject.AddComponent<FixedJoint>();
            // joint = go.gameObject.GetComponent<FixedJoint>();
            // print("joint is exixsting: ");
            joint.connectedBody = attatch;
            //joint.breakForce = breakForcee;
            go.gameObject.AddComponent<VibrateOnCollision>().trackedObj = trackedObj;

            var b = go.GetComponent<Boomerang>();
            var ham = go.GetComponent<HammerThrow>();
            if (isHammerThrowEnchanted || ham)
            {
                Destroy(ham);

            }
             
            if (!isBoomerangEnchanted || b)
            {
                Destroy(b);

            }
            ///////////////////////
            var magic = go.GetComponent<DoMagic>();
            if (go.gameObject.tag.Equals("EnchanterStone"))
            {
                if (!magic)
                {
                    magic = go.gameObject.AddComponent<DoMagic>();
                    magic.trackedObj = trackedObj;
                    magic.attatch = attatch.transform;
                    magic.prefab = go.gameObject.GetComponent<StoneStats>().prefab;

                }
            }

        }
         
           

        

    }
    public float charge = 0f;
    public float chargeDissapation = .010f;
    float yeet = 0;
    void OnTriggerStay(Collider c)
    {
        attatchedSwords = swordManager.GetComponent<SwordCount>().attSwords;
        var device = SteamVR_Controller.Input((int)trackedObj.index);
        var go = transform.gameObject;
        if (!holding && (c.gameObject.tag.Equals("Sword") || c.gameObject.tag.Equals("EnchanterStone")))
        {
            if (c.gameObject.GetComponent<Rigidbody>())
            {
                go = c.gameObject;

            }
            else
            {
                go = c.transform.parent.gameObject;


            }


            if ((!go.GetComponent<FixedJoint>() || go.GetComponent<stuck>()) && colliders.IndexOf(go) == -1)
            {
                colliders.Add(go);

            }
        }else if(holding && isChargedEnchanted)
        {
            joint.gameObject.GetComponent<Rigidbody>().mass = 1f;
           // print(Time.fixedDeltaTime);
            if (joint.gameObject.GetComponent<Offset>().isColliding)
            {

                yeet = 0;
            }

            if(yeet > .8f && !joint.gameObject.GetComponent<Offset>().isColliding)
            {
                
                device.TriggerHapticPulse((ushort)(charge *3f));

            }else
                 yeet+= Time.fixedTime;
               
           
            charge += device.velocity.magnitude*1.8f;
            if(charge > 0f)
                charge -= charge*chargeDissapation;

        }


        
        //TODO: sort the list
        for (int i = 0; i < colliders.Count - 1; i++)
        {
            var a = colliders[i];
            for(int j = i+1; j < colliders.Count; j++)
            {
                var b = colliders[j];
                var aDist = Vector3.Distance(a.transform.position, transform.position);
                var bDist = Vector3.Distance(b.transform.position, transform.position);

                if(aDist > bDist)
                {
                    colliders.RemoveAt(i);
                    colliders.Insert(j, a);

                }

                var aa = a.GetComponent<FixedJoint>();
                var bb = b.GetComponent<FixedJoint>();

                if (aa && !a.GetComponent<stuck>())
                    colliders.Remove(a);

                if (bb &&!b.GetComponent<stuck>())
                    colliders.Remove(b);
                

            }

        }



        //do pickup on the closest collider.
        GameObject closest;
        if (colliders.Count > 0)
            closest = colliders[0];
        else
            closest = null;

        if (colliders.Count > 0 && go.GetInstanceID() == closest.GetInstanceID() && device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger) && !holding && (!go.GetComponent<FixedJoint>() || go.GetComponent<stuck>()))
        {
            holding = true;
            pickUp(go);
            colliders.Remove(go);
        }
        



    }

	// Use this for initialization
	void Start () {
        attatchedSwords= swordManager.GetComponent<SwordCount>().attSwords;
        savedSwords = swordManager.GetComponent<SwordCount>().svdSwords;
	}
	
   public void throwObject()
    {
        var device = SteamVR_Controller.Input((int)trackedObj.index);
        var initVelocity = trackedObj.transform.parent.GetComponent<Rigidbody>().velocity;
        var newVelocity = Quaternion.Euler(0, rig.rotation.eulerAngles.y, 0) * device.velocity * (device.angularVelocity.magnitude / 30f) + initVelocity;
        newVelocity += newVelocity.normalized * charge/40f;
        if (holding &&(!device.GetPress(SteamVR_Controller.ButtonMask.Trigger)))
        {


        //    var initVelocity = trackedObj.transform.parent.GetComponent<Rigidbody>().velocity;
            print("THROWING BITCH");
            holding = false;
            var go = joint.gameObject;
            var rigidbody = go.GetComponent<Rigidbody>();

            rigidbody.drag = 0f;

            if(isHammerThrowEnchanted)
                go.AddComponent<HammerThrow>();
            if (isChargedEnchanted)
                rigidbody.mass = charge / 5f;
            Destroy(go.GetComponent<VibrateOnCollision>());
            Object.DestroyImmediate(joint);
           // Destroy(go.GetComponent<FixedJoint>());
            //print("joint is: " +go.GetComponent<FixedJoint>());
            joint = null;
           
            if (go.tag.Equals("EnchanterStone"))
            {
                Destroy(go.GetComponent<DoMagic>());
                go.GetComponent<StoneStats>().baseHeight = go.transform.position.y;
            }

            if(attatchedSwords.Count > 1 && false)
            {
                var origin = trackedObj.origin ? trackedObj.origin : trackedObj.transform.parent;
                rigidbody.useGravity = true;
                if (origin != null)
                {
                    rigidbody.velocity = origin.TransformVector(device.velocity * 1.5f) + initVelocity;
                    rigidbody.angularVelocity = origin.TransformVector(device.angularVelocity);
                }
                else
                {
                    rigidbody.velocity = 1.5f * device.velocity + initVelocity;
                    rigidbody.angularVelocity = device.angularVelocity;
                }
                // go.GetComponent<VibrateOnCollision>().enabled = false;
                rigidbody.maxAngularVelocity = rigidbody.angularVelocity.magnitude;
                if(attatchedSwords.IndexOf(rigidbody.gameObject) != 0)
                    attatchedSwords.Remove(rigidbody.gameObject);
            }
            else {
                if (isBoomerangEnchanted)
                {

                    //////////////////////////////////////////////////

                    var origin = trackedObj.origin ? trackedObj.origin : trackedObj.transform.parent;


                    //  var newVelocity = Quaternion.Euler(0, rig.rotation.eulerAngles.y, 0) * device.velocity *1.5f + initVelocity;
                    var newAngularVelocity = Quaternion.Euler(0, rig.rotation.eulerAngles.y, 0) * device.angularVelocity;
                    rigidbody.velocity = newVelocity;
                    rigidbody.angularVelocity = newAngularVelocity;


                    rigidbody.maxAngularVelocity = rigidbody.angularVelocity.magnitude;
                    //////////////////////////////////////////////////////////////////////////

                    if (go.GetComponent<Boomerang>())
                        Destroy(go.GetComponent<Boomerang>());
                    var b = go.AddComponent<Boomerang>();


                    // b.throwSpeed = device.velocity.magnitude *1.5f;
                    //   b.angularSpeed = device.angularVelocity.magnitude;
                    b.returnPosition = attatch.transform;
                    rigidbody.useGravity = false;


                }
                else
                {
                    ///  go.transform.parent = null;



                    // We should probably apply the offset between trackedObj.transform.position
                    // and device.transform.pos to insert into the physics sim at the correct
                    // location, however, we would then want to predict ahead the visual representation
                    // by the same amount we are predicting our render poses.

                    var origin = trackedObj.origin ? trackedObj.origin : trackedObj.transform.parent;
                    rigidbody.useGravity = true;
                    print((device.angularVelocity.magnitude/20f));
                    if (origin != null)
                    {
                        rigidbody.velocity = origin.TransformVector(device.velocity * (device.angularVelocity.magnitude/20f)) + initVelocity ;
                        rigidbody.angularVelocity = origin.TransformVector(device.angularVelocity);
                    }
                    else
                    {
                        rigidbody.velocity = (device.angularVelocity.magnitude/20f) * device.velocity + initVelocity;
                        rigidbody.angularVelocity = device.angularVelocity;
                    }
                    // go.GetComponent<VibrateOnCollision>().enabled = false;
                    rigidbody.maxAngularVelocity = rigidbody.angularVelocity.magnitude;
                }

            }
            
        }else if(holding && device.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
        {
            var dd = trackedObj.gameObject.transform.parent.GetComponent<Rigidbody>();
            Vector3 judir = (jumpDir.position - attatch.position).normalized;

           // GameObject go = (GameObject)Instantiate(jumpParticle, jumpDir.position, Quaternion.identity);
            //Destroy(go, 9f);
         //   dd.velocity = Vector3.zero;

         if(isJumpEnchanted && Physics.Raycast(transform.position, Vector3.down, 2f) && timer == 0f)
            {
                dd.velocity = (judir * (12f));
                timer++;
                StartCoroutine(reloadTimer());
            }
            

            var off = joint.gameObject.GetComponent<Offset>();
            if(off.particlePrefab!= null && timer == 0f)
            {
                var eggo = Instantiate(off.particlePrefab, off.magicPos.position, Quaternion.LookRotation(off.magicPos.position - off.offset.position));
                Destroy(eggo, 5f);
                StartCoroutine(joint.gameObject.GetComponent<VibrateOnCollision>().LongVibration(.1f, 1900f));
                timer++;
                StartCoroutine(reloadTimer());
            }




        }
        else if (holding && device.GetPress(SteamVR_Controller.ButtonMask.Trigger) && isJumpEnchanted)
        {

        }


    }
    float timer = 0f;
    IEnumerator reloadTimer()
    {
        yield return new WaitForSeconds(3f);
        timer = 0f;
    }
    // Update is called once per frame
    void FixedUpdate () {
     //   print("There are this many colliders: " + colliders.Count);
        //var farthest = colliders[colliders.Count - 1];
        //var closest = colliders[0];
       // print("Closest: " + closest.name);
        //print("Farthest: " + farthest.name);
          throwObject();
        var device = SteamVR_Controller.Input((int)trackedObj.index);
     

       

    }
}
