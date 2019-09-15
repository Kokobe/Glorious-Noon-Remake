using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoMagic : MonoBehaviour {
    public SteamVR_TrackedObject trackedObj;
    public Transform attatch;
    public GameObject prefab;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        var device = SteamVR_Controller.Input((int)trackedObj.index);

        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
           var go = Instantiate(prefab, attatch.position, attatch.rotation);
            Destroy(go, 10f);
        }

    }
}
