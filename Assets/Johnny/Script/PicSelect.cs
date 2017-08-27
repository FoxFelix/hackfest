using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PicSelect : MonoBehaviour {
	public PhotonView Select;
	public Vector3 OriginalScale;
	// Use this for initialization
	void Start () 
	{
		OriginalScale = gameObject.transform.localScale;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	void OnCollisionStay(Collision collision)
	{
		if (collision.transform.name == "Camera") 
		{
			gameObject.transform.localScale = Vector3.Lerp(gameObject.transform.localScale,new Vector3 (OriginalScale.x*1.2f, OriginalScale.y*1.2f, OriginalScale.z*1.2f),10*Time.deltaTime);
			gameObject.GetComponent<MeshRenderer> ().material.color = Color.Lerp (gameObject.GetComponent<MeshRenderer> ().material.color, Color.white, 10 * Time.deltaTime);
		}
	}
	void OnCollisionExit(Collision collision)
	{
		if (collision.transform.name == "Camera") 
		{
			gameObject.transform.localScale = new Vector3 (OriginalScale.x, OriginalScale.y, OriginalScale.z);
			gameObject.GetComponent<MeshRenderer> ().material.color = new Color (1f,1f,1f,0.6f);
		}
	}
}
