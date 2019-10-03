using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Spawn : MonoBehaviour {
    public onHeadCollision head;
    public onHeadCollision body;
    public Transform ninjaContainer;
    public float marginalDelayTime = 0f;
    public int maxAmtOfNinjas = 3;
    public int ninjasPerSpawn = 1;
    public GameObject[] prefabs;
    public Transform[] spawnLoc;
    public GameObject explosionParticles;
    public Transform player;
    public float waitTime = 20f;
    int ninjaCount = 0;
    public Image progBar;
    IEnumerator MakeNinjas()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(waitTime);
            waitTime += marginalDelayTime;
            Transform sLoc = (spawnLoc.Length != 0) ? spawnLoc[Random.Range(0, spawnLoc.Length)] : transform;

            var a = Instantiate(prefabs[Random.Range(0, prefabs.Length)], sLoc.position, transform.rotation);
            a.GetComponent<Ninja>().targett = player;
            a.transform.SetParent(ninjaContainer);

            ninjaCount++;

            var b = Instantiate(explosionParticles, sLoc.position, transform.rotation);
            Destroy(b, 10f);
            if(ninjaCount > 4)
            {
                waitTime += 10;
            }
            if (progBar)
            {
                var nin = a.GetComponent<Ninja>();
                nin.targett = player;
                nin.progressBar = progBar;
                nin.amount = .1f;
            }
        }


    }

	// Use this for initialization
	void OnEnable () {
        head.currentWave = gameObject;
        body.currentWave = gameObject;
        StartCoroutine(MakeNinjas());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
