using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PicSelect : MonoBehaviour {
	public PhotonView Select;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnCollisionEnter(Collision collision)
	{
		if (collision.transform.name == "Eye Beam Left") 
		{
			if (gameObject.transform.name == "lee") 
			{
				Select.RPC ("selectLee", PhotonTargets.All, false);
			}
			if (gameObject.transform.name == "wong") 
			{
				Select.RPC ("selectWong", PhotonTargets.All, false);
			}
			if (gameObject.transform.name == "du") 
			{
				Select.RPC ("selectDu", PhotonTargets.All, false);
			}
		}
	}
}
