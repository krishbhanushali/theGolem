using UnityEngine;
using System.Collections;

public class BackgroundMusic : MonoBehaviour {


    public AudioClip backgroundMusic;
    AudioSource sound;
	// Use this for initialization
	void Start () {
        sound = GetComponent<AudioSource>();
        sound.Play();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
