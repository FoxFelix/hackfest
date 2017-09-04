using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_audioPlayer : MonoBehaviour {

    public AudioSource audioHead;
    public AudioSource audioFootStep;
    public AudioClip IdleSound;
    public AudioClip ChaseSound;
    public AudioClip AttackSound;
    public AudioClip FootStepSound;
    public AudioClip DamageSound;

    public void BossPlayFootStep()
    {
        audioFootStep.PlayOneShot(FootStepSound);
    }

    public void BossPlayIdleAudio()
    {
        int i = Random.Range(0, 4);
        if (i == 1)
        {
            if (!audioHead.isPlaying)
                audioHead.PlayOneShot(IdleSound);
        }

    }

    public void BossPlayChaseSound()
    {
        int i = Random.Range(0, 2);
        if (i == 1)
        {
            if (!audioHead.isPlaying)
                audioHead.PlayOneShot(ChaseSound);
        }
        if (i == 0)
        {
            if (!audioHead.isPlaying)
                audioHead.PlayOneShot(IdleSound);
        }
    }
    public void BossPlayAttackSound()
    {
        if (audioHead.isPlaying)
            audioHead.Stop();
        audioHead.PlayOneShot(AttackSound);
    }

    public void BossPlayDamagedSound()
    {
        if (audioHead.isPlaying)
            audioHead.Stop();
        audioHead.PlayOneShot(DamageSound);
    }


}
