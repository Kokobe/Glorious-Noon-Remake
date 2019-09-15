using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStep : MonoBehaviour {
    public float delay = 0f;
    public bool StandOnIt = true;
    public bool triggerIt = false;
    public bool standOfIt = false;
    public bool basedOnDistance = false;
    public bool KillAllOfIt = false;
    public bool restock = false;
    public bool onEnablee = false;
   // public GameObject toRestock;
    public GameObject[] enemies;
    public GameObject manipulatedObject;
    public onHeadCollision playerBod;
    public onHeadCollision playerHead;
    public bool makeInactive = true;
    public bool makeActive = false;
    public bool toggleInactive = false;
    public bool destroyIt = false;
    public bool toggleKinematic = false;
    public bool explodeRock = false;
    public bool instantiateIt = false;
    public bool loadLevel = false;
    public string levelName;
    public bool enableTeleport = false;
    public bool addRewards = false;
    public bool returnRewards = false;
    public VRTK.VRTK_DashTeleport tele;
    public Transform instantationParent;
    public Transform instantiationPlace;


    // Use this for initialization
    
    void OnEnable()
    {
        if (onEnablee)
        {
            StartCoroutine(Actions());
        }
    }

    IEnumerator checkIfAlive()
    {
        int bodyCount = 0;
        while (true)
        {
            yield return new WaitForSecondsRealtime(10f);

            foreach (GameObject go in enemies)
            {
                if (go == null)
                    bodyCount++;
            }
            print("body count: " + bodyCount);
            print("Enemy Length:" + bodyCount);
            if (bodyCount == enemies.Length)
            {
                StartCoroutine(Actions());
                break;
            }else
            {
                bodyCount = 0;
            }

        }
    }
    static OnStep s = null;
    void Start()
    {
        if (KillAllOfIt)
            StartCoroutine(checkIfAlive());

        if (loadLevel)
        {
            if (s == null)
                s = this;
           // else
               // Destroy(this.gameObject);

           // DontDestroyOnLoad(this.gameObject);

        }

    }

   public IEnumerator Actions()
    {
        yield return new WaitForSecondsRealtime(delay);
        if (makeInactive && manipulatedObject !=null)
            manipulatedObject.SetActive(false);
        else if (toggleInactive)
        {
            manipulatedObject.SetActive(manipulatedObject.active ? false : true);
        }else if (makeActive)
        {
            manipulatedObject.SetActive(true);
            if (playerBod && playerHead)
            {
                playerBod.currentWave = manipulatedObject ;
                playerHead.currentWave = manipulatedObject;
            }


        }
        if (destroyIt)
            Destroy(manipulatedObject);
        if (toggleKinematic)
        {
            var r = manipulatedObject.GetComponent<Rigidbody>();
            r.isKinematic = false;
        }

        if (explodeRock)
        {
            //print("YAY");
            var br = manipulatedObject.GetComponent<Breakable_Object>();
            br.enabled = true;
         
            br.Explode(manipulatedObject.transform.position, 0f, 0);


        }

        if (instantiateIt)
        {
            var ggo = Instantiate(manipulatedObject, instantiationPlace.position, Quaternion.identity);
            if (instantationParent)
            {
                ggo.transform.parent = instantationParent;
            }
            if (restock)
            {
                enemies[0] = ggo;
                StartCoroutine(checkIfAlive());
            }
        }


        if (loadLevel)
        {
            SteamVR_LoadLevel.Begin(levelName);
        }

        if (enableTeleport)
        {
            tele.enabled = true;
        }

        if (addRewards)
        {
            GameControl.control.Save(manipulatedObject);
        }

        if (returnRewards)
        {
            GameControl.control.Load();
            GameObject ggo;
            if (GameControl.control.SavedSwords != null)
                ggo = Instantiate(GameControl.control.SavedSwords, instantiationPlace.position, Quaternion.identity); 
        }

        yield return null;


    }

    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag.Equals("Player") && StandOnIt)
        {

            StartCoroutine(Actions());

        }
    }

    void OnTriggerEnter(Collider c)
    {
        if(c.gameObject != null && (c.gameObject.tag.Equals("Sword") || c.attachedRigidbody.gameObject.tag.Equals("Player") || c.gameObject.tag.Equals("Balloon")) && triggerIt)
        {
            print("GOING:" + c.gameObject.name);
            StartCoroutine(Actions());
           
        }


    }
       void OnCollisionExit (Collision c) {
            if (c.gameObject.tag.Equals("Player") && standOfIt)
            {
            StartCoroutine(Actions());

        }
        }
    
        // Update is called once per frame
        void Update () {
		
	}
}
