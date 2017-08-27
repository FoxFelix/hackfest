using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footStepPlaySound : MonoBehaviour {
    public AudioSource audioplayer;
    public AudioClip footStepSound;

    public void playFootStepSound()
    {
        audioplayer.Play();
    }
}
