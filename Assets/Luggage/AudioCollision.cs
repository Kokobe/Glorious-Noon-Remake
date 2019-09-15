using UnityEngine;
using System.Collections;

public class AudioCollision : MonoBehaviour {

    public AudioClip[] collisionAudios;
    public GameObject soundprefab;
   // public AudioClip[] swooshAudios;
    private AudioSource source; private Rigidbody r;
	// Use this for initialization
	void Start () {

       // source = GetComponent<AudioSource>();
       
	}

    void OnCollisionEnter(Collision c)
    {
        var go = Instantiate(soundprefab, c.transform.position, Quaternion.identity);
        go.transform.parent = transform;
        source = go.GetComponent<AudioSource>();
        Destroy(go, 5f);

        int index = Random.Range(0, collisionAudios.Length);
        source.clip = collisionAudios[index];
        source.Play(0);

    }

	
	// Update is called once per frame
	void Update () {
	
     

	}
}
