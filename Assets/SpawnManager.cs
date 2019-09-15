using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpawnManager : MonoBehaviour {
    public onHeadCollision head;
    public onHeadCollision body;
    public Transform ninjaContainer;
    public Transform[] spawnPts;
    public GameObject[] ninjas;
    public GameObject[] startingNinjas;
    public Image progBar;
    public TextMesh waveText;
    //public GameObject[] ninjasAlive;
    public float marginalDelayTime = 0f;
    public int maxAmtOfNinjas = 3;
    public int[] ninjasPerSpawn;
  //  public GameObject[] prefabs;
    public GameObject explosionParticles;
    public Transform player;
    public float waitTime = 10f;
    public float marginalWaitTime = .5f;
    public int ninjaCount = 0;
    public int roundNum = 0;
    public GameObject[] waveEndObjs;
    IEnumerator checkIfDone()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            if(progBar.fillAmount >= 1)
            {
                string wt = waveText.text;
                waveText.text = wt.Substring(0, wt.Length- 1)+ (int.Parse(wt.Substring(waveText.text.Length-1)) +1);
                progBar.fillAmount = 0;
                foreach(GameObject go in waveEndObjs)
                {
                    go.SetActive(true);
                    
                }
                StopCoroutine(MakeNinjas());
                yield return new WaitForSecondsRealtime(16.1f);
                progBar.fillAmount = 0;
                ninjaCount = 0;
                roundNum = 0;
                gameObject.transform.parent.gameObject.SetActive(false);
                break;


            }
        }
    }

    IEnumerator MakeNinjas()
    {
        maxAmtOfNinjas = ninjas.Length;
        float progAmt = 1f / (ninjas.Length + startingNinjas.Length);

        if (startingNinjas.Length != 0)
        {
            foreach(GameObject a in startingNinjas)
            {
                var nin = a.GetComponent<Ninja>();
                //nin.targett = player;
                nin.progressBar = progBar;
                nin.amount = progAmt;
                nin.transform.SetParent(ninjaContainer);    
                //  ninjasAlive[ninjaCount] = a;

                var b = Instantiate(explosionParticles, a.transform.position, transform.rotation);
                Destroy(b, 10f);

            }

        }
      
    //    ninjasAlive = new GameObject[ninjas.Length];
        while (ninjaCount < maxAmtOfNinjas)
        {
           

            for(int i = 0; i < ninjasPerSpawn[roundNum]; i++)
            {
                Vector3 spwnLoc = spawnPts[Random.Range(0, spawnPts.Length)].position;
                var a = Instantiate(ninjas[ninjaCount], spwnLoc, transform.rotation);
                var nin = a.GetComponent<Ninja>();
                nin.targett = player;
                nin.progressBar = progBar;
                nin.amount = progAmt;
                //  ninjasAlive[ninjaCount] = a;
                nin.transform.SetParent(ninjaContainer);

                var b = Instantiate(explosionParticles, spwnLoc, transform.rotation);
                Destroy(b, 10f);
                
                yield return new WaitForSeconds(marginalWaitTime);
                ninjaCount++;
            }

            roundNum++;
            yield return new WaitForSecondsRealtime(waitTime);
            waitTime += marginalDelayTime;

        }

        


    }
    
    // Use this for initialization
    void OnEnable()
    {
        progBar.fillAmount = 0;
        ninjaCount = 0;
        roundNum = 0;
        head.currentWave = gameObject;
        body.currentWave = gameObject;
        StartCoroutine(checkIfDone());
        StartCoroutine(MakeNinjas());
    }
}
