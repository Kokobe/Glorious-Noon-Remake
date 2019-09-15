using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sail : MonoBehaviour {
	float tPosition = 0;
    public Transform forward;
    float tAngle = 0;
	public float radius; 
	public float speed;
	public Transform[] cannons;
	public GameObject playa;
	public float stopDistance;
    public float distFromPort;
    public Transform port;
	float invSpeed;
	public GameObject cannonBallPrefab;
	public bool shooting = false;
	public bool BattleMode = false;
    public Vector3 initialPos;
    public bool isPlayerNear;
    public bool isAtPort;
    // Use this for initialization
    void Start () {
		invSpeed = 1 / speed;
	}

	Vector3 ClosestCannon(){
		var closest = cannons [0];
		var j = 0;
		for (int i = 1; i < cannons.Length; i++) {

			float cannonAdistance = Vector3.Distance (playa.transform.position, cannons [i].position);
			float closestDistance = Vector3.Distance (playa.transform.position,closest.position);

			if (closestDistance > cannonAdistance) {
				closest = cannons [i];
			}


		}



		return closest.position;


	}
	float gg = 0;

	IEnumerator resetGG(){
		Time.timeScale = .5f;
		yield return new WaitForSecondsRealtime (3f);
		Time.timeScale = 1f;
		gg = 0;

	}
	IEnumerator shootCannonBalls(){
        shooting = true;
		gg += Time.deltaTime * 3f;
		var can = ClosestCannon ();
		var ball = Instantiate (cannonBallPrefab, can, Quaternion.identity);
		ball.transform.parent = transform;
        if (gg > 2f)
            gg = 1.9f;
		yield return new WaitForSeconds (2f- gg);

		if (gg == 1.9f) {
			StartCoroutine (resetGG());

		}


		ball.transform.parent = null;
		ball.GetComponent<Rigidbody>().velocity =  (playa.transform.position -ball.transform.position).normalized *15f ;
		shooting = false;
		Destroy (ball, 10f);
	}


    float multiplier = 1f;

    IEnumerator changeMultiplier()
    {
        multiplier = 1f;
        yield return new WaitForSecondsRealtime(5f);
        float t = 0f;
        while (true)
        {
            multiplier = Mathf.Cos(t);
            t += Time.smoothDeltaTime / 10f;
            yield return null;
        }

    }
	
	// Update is called once per frame
	void FixedUpdate () {

        isPlayerNear = Vector3.Distance(playa.transform.position, transform.position) < stopDistance;
        isAtPort = Vector3.Distance(port.position, transform.position) < distFromPort;

        if (!BattleMode)
        {
            if (!isAtPort || !isPlayerNear)
            {
                tPosition += Time.smoothDeltaTime / speed;
                transform.position = new Vector3(radius * Mathf.Cos(tPosition), 0, (radius + 10f) * Mathf.Sin(tPosition)) + initialPos;
                transform.rotation = Quaternion.Euler(new Vector3(-90, 0, -tPosition * 180f / Mathf.PI));
            }else if(isAtPort && isPlayerNear)
            {

                BattleMode = true;
                tAngle = tPosition;
                //StartCoroutine(shootCannonBalls());
             //   StartCoroutine(changeMultiplier());
            }

        }else
        {

            if (!shooting)
            {
                StartCoroutine(shootCannonBalls());
            }




            tAngle += Time.smoothDeltaTime / 10f;
          //  tAngle *= 1.002f;
                transform.rotation = Quaternion.Euler(new Vector3(-90f, 0, -tAngle * 180f / Mathf.PI * multiplier));
            

            if (!isPlayerNear)
            {
                BattleMode = false;
                multiplier = 1f;
            }
               



        }
        


        
        


        

		

	}
}
