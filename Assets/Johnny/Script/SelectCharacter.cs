using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacter : MonoBehaviour {
	public string NowSelect;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{        
		
	}
	void OnCollisionStay(Collision collision)
	{
		if (collision.transform.tag == "Character") 
		{
			NowSelect = collision.transform.name;
		}
	}
}
