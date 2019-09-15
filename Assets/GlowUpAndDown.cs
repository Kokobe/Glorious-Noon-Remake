using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowUpAndDown : MonoBehaviour {
    public float emission = 1.0f;
    public float f = 1.0f;
   public float t = 0f;
    public SteamVR_TrackedObject trackedObj;
    public IEnumerator LongVibration(float length, float strength)
    {
        var device = SteamVR_Controller.Input((int)trackedObj.index);
        for (float i = 0; i < length; i += Time.fixedDeltaTime)
        {
            device.TriggerHapticPulse((ushort)Mathf.Lerp(0, strength, 300f));

            yield return null;

        }

    }

    void OnCollisionExit(Collision c)
    {
        if (c.gameObject.tag.Equals("Ninja"))
             t -= .02f;
        if (c.gameObject.GetComponent<Elephant>())
        {
            t -= .8f;
           
        }

    }

  
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    
	void Update () {

        Renderer renderer = GetComponent<Renderer>();
        Material mat = renderer.materials[1];

        float floor = 0.0f;
        float ceiling = 1.0f;

        emission =  Mathf.Lerp(floor, ceiling, t);
        if(t < 1.0f)
        {
            if(t < -.5f)
                t += Time.deltaTime / 5f;
            else
                t += Time.deltaTime / 30f;

        }
           
        Color baseColor = new Color(1, 0.9521298f, 0.5661765f) ; //Replace this with whatever you want for your base color at emission level '1'

        Color finalColor = baseColor * Mathf.LinearToGammaSpace(emission);

        mat.SetColor("_EmissionColor", finalColor);


        if (t < -.5f)
        {
            GetComponent<Breakable_Object>().enabled = true;
            StartCoroutine(LongVibration(.1f, 3000f));
        }else if(t < -1f)
        {
            GetComponent<Breakable_Object>().Explode(transform.position, 300f, 0f);
        }
        else
        {
            GetComponent<Breakable_Object>().enabled = false;
        }

    }
}
