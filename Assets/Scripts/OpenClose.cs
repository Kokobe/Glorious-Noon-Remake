using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenClose : MonoBehaviour {

	 Animator anim;
	bool isClosed = false;
    public SteamVR_TrackedObject trackedObj;
  
    // Use this for initialization
    void Start () {
		anim = GetComponent<Animator> ();
       
	}
	


	// Update is called once per frame
	void Update () {
        var device = SteamVR_Controller.Input((int)trackedObj.index);

        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {

            anim.Play("Grab", -1, 0f);
        }
            if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                anim.Play ("Grab 0", -1, 1f);
            //    anim.playbackTime = 1f;
				isClosed = false;

			
		}


	}
}
