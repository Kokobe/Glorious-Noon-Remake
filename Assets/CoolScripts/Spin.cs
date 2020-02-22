using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour {

    public Transform rig;
    public Transform head;
    public float desiredDegree = 0;
    public float initialDegree = 0f;

    [SerializeField]
    protected OVRInput.Controller m_controller;
    private bool readyToRotateAgain = true;
    bool moveLeft;
    bool moveRight;
    bool rotating = false;
    float t = 0f;
	// Use this for initialization
	void Start () {
        desiredDegree = rig.rotation.eulerAngles.y;
       
    }

    IEnumerator spinIt()
    {
        var difference = (rig.rotation.eulerAngles.y) - desiredDegree % 360;
        while (t < Mathf.Abs(desiredDegree))
        {
            t += Mathf.Abs(desiredDegree / 10f);
            rig.RotateAround(head.position, new Vector3(0, 1, 0), desiredDegree/10f);

           
            yield return null;
        }

        readyToRotateAgain = true;

    }
    
    void Update()
    {

        if (readyToRotateAgain)
        {
           
            if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickLeft) || Input.GetKeyDown(KeyCode.E))
            {
                
                desiredDegree = -60;
                readyToRotateAgain = false;
           
                t = 0f;
                StartCoroutine(spinIt());
            }
            else if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickRight)) { 
                desiredDegree = 60;
                readyToRotateAgain = false;
               
                t = 0f;
                StartCoroutine(spinIt());
            }

          

        }
        else
        {
       //    if (Rightdevice.GetAxis().sqrMagnitude ==0 && Leftdevice.GetAxis().sqrMagnitude ==0)
         //       readyToRotateAgain = true;

        }
        //  rig.rotation = Quaternion.RotateTowards(rig.rotation, Quaternion.Euler(0, degree, 0), Time.unscaledDeltaTime * 500f);

        

      
    }


  
}
