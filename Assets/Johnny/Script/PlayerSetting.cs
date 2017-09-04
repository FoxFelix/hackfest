using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetting : MonoBehaviour {
	public GameObject Model;
	public Vector3 StartPos;
	// Use this for initialization
	void Start () 
	{
		gameObject.transform.position = StartPos;
		if (GetComponent<PhotonView> ().isMine) 
		{
			Model.SetActive (false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
