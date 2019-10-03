using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTexting : MonoBehaviour {


    public TextMesh t;
    public GameObject firstTarget;
    public GameObject secondTargetParent;
    public GameObject thirdTargetParent;
    public GameObject fourthTargets;
    public GameObject finishedWaves;
    public GameObject teleportObj;

    bool checkIfThere(GameObject[] arr)
    {
        foreach (GameObject g in arr){
            if (g != null)
                return true;
        }
        return false;
    }

    IEnumerator StartTutorial()
    
    {
       
        t.text = "My child, grab your sword with the \ntrigger and throw it at that target.";
            
        firstTarget.SetActive(true);
        while(firstTarget != null)
        {
            yield return null;
        }
        t.text = "Good. Very good. \nNow try these targets.";
        secondTargetParent.SetActive(true);
        while (secondTargetParent.transform.childCount != 0)
        {
            yield return null;
        }
        t.text = "You are glorious, my child. Now let me \nshow you the demons. Aim for the\ntargets on their body. (Tip: The more you \nmove your sword hand, the stronger your\nthrow. Do you feel the rumbling power?";
        thirdTargetParent.SetActive(true);
        while (thirdTargetParent.transform.childCount != 0)
        {
            yield return null;
        }
        t.text = "I have blessed you with symbol on your \nleft hand. If its light dies, you will die on the next attack. \nAlso, press the touchpad to move.";
        yield return new WaitForSecondsRealtime(5f);
        fourthTargets.SetActive(true);
        teleportObj.SetActive(true);
        
        while(finishedWaves.activeSelf == false)
        {
            yield return null;
        }

        t.text = "Go forth carefully, my child.";
    }

    // Use this for initialization
    void OnEnable()
    {
        thirdTargetParent.SetActive(false);
        StartCoroutine(StartTutorial());
    }

    // Update is called once per frame
    void Update () {
		
	}
}
