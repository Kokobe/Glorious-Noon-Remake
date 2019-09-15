using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpAndBoommerang : MonoBehaviour {
    public Rigidbody attatch;
    public float breakForcee;
    public SteamVR_TrackedObject trackedObj;
    public FixedJoint joint;

    public virtual void pickUp(Collider c)
    {
        var device = SteamVR_Controller.Input((int)trackedObj.index);
        var go = transform;
        var offRot = Vector3.up;



        if (joint == null && c.gameObject.tag.Equals("Sword") && device.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            if (c.gameObject.GetComponent<Rigidbody>())
            {
                go = c.gameObject.transform;

            }
            else
            {
                go = c.gameObject.transform.parent;
               

            }





            // c.gameObject.transform.rotation = attatch.rotation;
            //c.gameObject.transform.parent = transform;

            if (!go.gameObject.GetComponent<FixedJoint>())
            {

                go.transform.rotation = attatch.rotation;

                //  go.transform.rotation = Quaternion.Euler(go.rotation.eulerAngles.x + offRot.x, go.rotation.eulerAngles.y + offRot.y, go.rotation.eulerAngles.z + offRot.z);
                go.position = attatch.position + go.position - go.GetComponent<Offset>().offset.position;


                joint = go.gameObject.AddComponent<FixedJoint>();
                joint.connectedBody = attatch;
                //joint.breakForce = breakForcee;
                go.gameObject.AddComponent<VibrateOnCollision>().trackedObj = trackedObj;


            }



        }

    }

    void OnTriggerStay(Collider c)
    {
        pickUp(c);





    }

    // Use this for initialization
    void Start()
    {

    }

    public virtual void throwObject()
    {
        var device = SteamVR_Controller.Input((int)trackedObj.index);
        print(joint == null);
        if (joint != null && device.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
        {
            var go = joint.gameObject;
            var rigidbody = go.GetComponent<Rigidbody>();
            Destroy(go.GetComponent<VibrateOnCollision>());
            Object.DestroyImmediate(joint);
            joint = null;
            ///  go.transform.parent = null;



            // We should probably apply the offset between trackedObj.transform.position
            // and device.transform.pos to insert into the physics sim at the correct
            // location, however, we would then want to predict ahead the visual representation
            // by the same amount we are predicting our render poses.

            var origin = trackedObj.origin ? trackedObj.origin : trackedObj.transform.parent;
            if (origin != null)
            {
                rigidbody.velocity = origin.TransformVector(device.velocity * 1.5f);
                rigidbody.angularVelocity = origin.TransformVector(device.angularVelocity);
            }
            else
            {
                rigidbody.velocity = 1.5f * device.velocity;
                rigidbody.angularVelocity = device.angularVelocity;
            }
            // go.GetComponent<VibrateOnCollision>().enabled = false;
            rigidbody.maxAngularVelocity = rigidbody.angularVelocity.magnitude;
        }


    }

    // Update is called once per frame
    void Update()
    {

        throwObject();



    }
}
