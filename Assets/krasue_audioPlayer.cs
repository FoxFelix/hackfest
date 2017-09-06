using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class krasue_audioPlayer : MonoBehaviour {

    public AudioSource audioHead;
    public AudioClip IdleSound;
    public AudioClip AttackSound;
    public AudioClip DamageSound;

    public void PlayIdleAudio()
    {
        int i = Random.Range(0, 2);
        if (i == 1)
        {
            if (!audioHead.isPlaying)
                audioHead.PlayOneShot(IdleSound);
        }
    }

    public void PlayAttackAudio()
    {
        if (audioHead.isPlaying)
        {
            audioHead.Stop();
            audioHead.PlayOneShot(AttackSound);
        }
    }
    public void PlayDamageAudio()
    {
        if (audioHead.isPlaying)
        {
            audioHead.Stop();
            audioHead.PlayOneShot(DamageSound);
        }
    }
}
