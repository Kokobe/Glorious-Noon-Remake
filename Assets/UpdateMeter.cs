using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateMeter : MonoBehaviour {
	public Image healthBar;
	public float fillSpeed = .1f;
	public float unfillSpeed = .1f;
	bool done = false;
	bool filling = false;
    public GameObject checkMark;
	void OnTriggerStay(Collider c){
       
		if (c.attachedRigidbody.gameObject.tag.Equals("Player") && !done) {
			filling = true;
			healthBar.fillAmount += fillSpeed * Time.deltaTime;
		}
			
	}

	void OnTriggerExit(Collider c){
		filling = false;
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!filling && !done) {
			healthBar.fillAmount -= unfillSpeed * Time.deltaTime;
		}

		if (healthBar.fillAmount == 1) {
			done = true;
            checkMark.SetActive(true);
           // StartCoroutine(healthBar.transform.parent.gameObject.GetComponent<OnStep>().Actions());
            StartCoroutine(healthBar.gameObject.GetComponent<OnStep>().Actions());
        }
	}
}
