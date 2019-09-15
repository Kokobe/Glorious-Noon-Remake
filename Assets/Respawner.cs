using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour {
    public GameObject Cam;
    public GameObject prefab;

    public GameObject[] prefabs;
    Vector3[] positions;
    Quaternion[] rotations;
    Vector3[] sizes;
    float[] expPowerOnPlayer;
	// Use this for initialization
	void Start () {
        positions = new Vector3[prefabs.Length];
        rotations = new Quaternion[prefabs.Length];
        sizes = new Vector3[prefabs.Length];
        expPowerOnPlayer = new float[prefabs.Length];
        for (int i = 0; i<prefabs.Length; i++)
        {
            positions[i] = prefabs[i].transform.position;
            rotations[i] = prefabs[i].transform.rotation;
            sizes[i] = prefabs[i].transform.localScale;
            prefabs[i].transform.GetChild(1).GetComponent<Breakable_Object>().setCam(Cam);
            expPowerOnPlayer[i] = prefabs[i].transform.GetChild(1).GetComponent<Breakable_Object>().powerOnPlayer;

        }
        StartCoroutine(checkForDeadGameObjects());
	}

    IEnumerator checkForDeadGameObjects()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(10f);

            for (int i = 0; i < prefabs.Length; i++)
            {

                if (prefabs[i] == null)
                {
                    var go = Instantiate(prefab, positions[i], rotations[i]);
                    go.transform.localScale = sizes[i];
                    go.SetActive(false);
                    go.SetActive(true);
                    prefabs[i] = go;
                    prefabs[i].transform.GetChild(1).GetComponent<Breakable_Object>().setCam(Cam);
                    prefabs[i].transform.GetChild(1).GetComponent<Breakable_Object>().powerOnPlayer = expPowerOnPlayer[i];
                    // go.GetComponent<Breakable_Object>().power *= 1.5f;

                }
            }

          //  yield return null;
        }
    }




	

}