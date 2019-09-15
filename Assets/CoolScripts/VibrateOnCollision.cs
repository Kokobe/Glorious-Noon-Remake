using UnityEngine;
using System.Collections;

public class VibrateOnCollision : MonoBehaviour {
    public SteamVR_TrackedObject trackedObj;
    public GameObject object1;
    public GameObject object2;
    ConfigurableJoint joint;
    // Use this for initialization
    void Start()
    {
 
    }

    void OnCollisionEnter(Collision c)
    {
        var device = SteamVR_Controller.Input((int)trackedObj.index);
        /*
        if (GetComponent<ConfigurableJoint>())
        {
            joint.xMotion = ConfigurableJointMotion.Limited;
            joint.yMotion = ConfigurableJointMotion.Limited;
            joint.zMotion = ConfigurableJointMotion.Limited;
        }

        */

        StartCoroutine(LongVibration(.1f, 1900f));
        /// c.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(device.velocity * 300f, c.contacts[0].point);



    }

    void Update()
    {
      

    }

   public IEnumerator LongVibration(float length, float strength)
    {
        var device = SteamVR_Controller.Input((int)trackedObj.index);
        for (float i = 0; i < length; i += Time.fixedUnscaledDeltaTime)
        {
            device.TriggerHapticPulse((ushort)Mathf.Lerp(0, strength, 300f));

            yield return null;

        }

    }
  
    
}
