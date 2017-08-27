using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundMusicControl : MonoBehaviour {
    public AudioSource audioPlayer;
	
	void Start () {
		
	}
	
    void OnTriggerEnter()
    {
        audioPlayer.Play();
        audioPlayer.loop = true;
    }

    void OnTriggerExit()
    {
        audioPlayer.loop = false;

    }

	// Update is called once per frame
	void Update () {
		
	}
}
