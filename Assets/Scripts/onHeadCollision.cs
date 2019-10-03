using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;
public class onHeadCollision : MonoBehaviour {
    public bool canDie = true;
    public GameObject currentWave;
    public GameObject waveDisplay;
    public Transform NinjaContainer;
    public GameObject boundary;
    public bool inChargeOfRespawn = false;
    public GameObject swordManagerr;
    public GlowUpAndDown gl;
    public Rigidbody shieldAttatch;
    public GameObject shield;
    public GameObject shieldPrefab;
    public GameObject leftHand;
    public Vector3 shieldPos;
    public Quaternion shieldRot;
    public GameObject eyes;
    public Shader grayShader;
    public bool dead = false;
    public Transform respawnPlace;
    public Transform swordRespawnPlace;
    public float damage = .08f;
    public GameObject camRig;
    public int currLevel = 0;
    public string[] levelNames = new string[2] { "MainScene", "Death" };
    
	// Use this for initialization

    void OnCollisionEnter(Collision c)
    {
      

        if (shield && c.gameObject.tag.Equals("Ninja"))
        {
            gl.t -= damage;
            StartCoroutine(gl.LongVibration(.1f, 1900f));
            if (c.gameObject.GetComponent<Elephant>())
            {
                gl.t -= .8f;
            }
        }
           
        else if (!shield && !dead && c.gameObject.tag.Equals("Ninja") && inChargeOfRespawn)
        {
            if (canDie)
            {
                dead = true;
                var gray = eyes.AddComponent<Grayscale>();
                gray.shader = grayShader;
                StartCoroutine(Respawn(gray));
            }
            
        }
    }


    IEnumerator Respawn(Grayscale g)
    {
        yield return new WaitForSecondsRealtime(3f);
        swordManagerr.transform.position = respawnPlace.position;
        swordManagerr.transform.rotation = respawnPlace.rotation;
        g.enabled = false;
        dead = false;
        var SM = swordManagerr.GetComponent<SwordCount>();
        if (SM.svdSwords.Count != 0)
        {
            for(int i =0; i < SM.svdSwords.Count; i++)
            {
                var swordgo = swordManagerr.GetComponent<SwordCount>().svdSwords[i];
                swordgo.transform.position = swordRespawnPlace.position + Vector3.right * i;
                Destroy(swordgo.GetComponent<FixedJoint>());
                swordgo.GetComponent<Rigidbody>().velocity = Vector3.zero;
                swordgo.GetComponent<Rigidbody>().drag = 10f;
                Destroy(swordgo.GetComponent<Boomerang>());
                Destroy(swordgo.GetComponent<VibrateOnCollision>());
            }
           

          
        }
        SM.attSwords.Clear();
        SM.svdSwords.Clear();
        for (int i = 0; i < NinjaContainer.childCount; i++)
        {
            Destroy(NinjaContainer.GetChild(i).gameObject);
        }
        currentWave.SetActive(false);
        waveDisplay.SetActive(false);
        var shieldGO = Instantiate(shieldPrefab, shieldPos, shieldRot, leftHand.transform);
        shieldGO.transform.localPosition = shieldPos;
        shieldGO.transform.localRotation = shieldRot;
        shieldGO.GetComponent<GlowUpAndDown>().trackedObj = leftHand.GetComponent<SteamVR_TrackedObject>();
        shield = shieldGO;
        gl = shieldGO.GetComponent<GlowUpAndDown>();
        camRig.GetComponent<onHeadCollision>().gl = gl;
        camRig.GetComponent<onHeadCollision>().shield = shield;

        shieldGO.GetComponent<FixedJoint>().connectedBody = shieldAttatch;
        shieldGO.GetComponent<VibrateOnCollision>().trackedObj = leftHand.GetComponent<SteamVR_TrackedObject>();
        shieldGO.transform.parent = null;

        boundary.SetActive(true);

    }
    IEnumerator selectRotation()
    {
        yield return new WaitForSeconds(10);
        shieldRot = shield.transform.localRotation;
    }
	void Start () {
        shieldPos = shield.transform.localPosition;
        StartCoroutine(selectRotation());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
