using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vibrateMaster : MonoBehaviour {
    public SteamVR_TrackedObject right;
    public SteamVR_TrackedObject left;
    public float rightPower = 0;
    public float leftPower = 0;
    public float duration = .5f;
     float t = 0f;
    public bool collision = false;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        var rDevice = SteamVR_Controller.Input((int)right.index);
        var lDevice = SteamVR_Controller.Input((int)left.index);

        if (collision|| rDevice.GetPress(SteamVR_Controller.ButtonMask.Grip))
        {
        rDevice.TriggerHapticPulse((ushort)Mathf.Lerp(0, rightPower, 1));

         }

        if (collision|| lDevice.GetPress(SteamVR_Controller.ButtonMask.Grip))
        {
            lDevice.TriggerHapticPulse((ushort)Mathf.Lerp(0, leftPower, 1));

        }
       

        if(t > duration)
        {
            collision = false;
            t = 0;

        }else if(collision)
        {
            t += Time.fixedDeltaTime;
        }
    }
}
