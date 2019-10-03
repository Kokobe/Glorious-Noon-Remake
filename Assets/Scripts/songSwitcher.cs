using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class songSwitcher : MonoBehaviour {
    public AudioClip music1;
    public AudioClip music2;
    public AudioClip[] musics;
    int i = 0;
    private AudioSource source;
    // Use this for initialization
    void Start () {
        source = GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update () {
        if(!source.isPlaying && source.clip.Equals(music1))
        {
            source.clip = music2;
            source.PlayOneShot(music2);
        }

        if (!source.isPlaying && source.clip.Equals(music2))
        {
            source.clip = music1;
            source.PlayOneShot(music1);
        }

        if (!source.isPlaying)
        {
            i += 1;
            if(i >musics.Length)
            {
                i = 0;
                
            }
            source.clip = musics[i];
            source.PlayOneShot(musics[i]);
        }

    }
}
