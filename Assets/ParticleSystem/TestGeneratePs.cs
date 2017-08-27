using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGeneratePs : MonoBehaviour 
{
    private Vector3 pos = new Vector3(0,0,0);
    public Transform ts;
    public ResourceEffect m_RsEffect = null;

	void Start () 
    {
	}

	void Update () 
    {
		if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            //Debug.Log("Generate Ps");
            ParticleManager.GenerateParticle(m_RsEffect.m_Effect[0], ts);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //Debug.Log("Generate Ps");
            ParticleManager.GenerateParticle(m_RsEffect.m_Effect[1], ts);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //Debug.Log("Generate Ps");
            ParticleManager.GenerateParticle(m_RsEffect.m_Effect[2], ts);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            //Debug.Log("Generate Ps");
            ParticleManager.GenerateParticle(m_RsEffect.m_Effect[3], ts);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            //Debug.Log("Generate Ps");
            ParticleManager.GenerateParticle(m_RsEffect.m_Effect[4], ts);
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            //Debug.Log("Generate Ps");
            ParticleManager.GenerateParticle(m_RsEffect.m_Effect[5], ts);
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            //Debug.Log("Generate Ps");
            ParticleManager.GenerateParticle(m_RsEffect.m_Effect[6], ts);
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            //Debug.Log("Generate Ps");
            ParticleManager.GenerateParticle(m_RsEffect.m_Effect[7], ts);
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            //Debug.Log("Generate Ps");
            ParticleManager.GenerateParticle(m_RsEffect.m_Effect[8], ts);
        }
	}
}
