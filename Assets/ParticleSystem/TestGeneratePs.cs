using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGeneratePs : MonoBehaviour 
{
    private Vector3 pos = new Vector3(0,0,0);
    public Transform ts;
    public PsUnit FireEffect = null;
	// Use this for initialization
	void Start () 
    {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
		if(Input.GetKeyDown(KeyCode.P))
        {
            //Debug.Log("Generate Ps");
            ParticleManager.GenerateParticle(FireEffect, ts);
        }
	}
}
