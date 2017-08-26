using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetting : MonoBehaviour {
	public GameObject StartPoint;
	// Use this for initialization
	void Start () 
	{
		StartPoint = GameObject.Find ("StartPoint");
		gameObject.transform.position = StartPoint.transform.position;	
		StartPoint.transform.position = new Vector3 (StartPoint.transform.position.x+1.5f,StartPoint.transform.position.y,StartPoint.transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
