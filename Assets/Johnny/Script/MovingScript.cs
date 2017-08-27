using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingScript : MonoBehaviour {
	public Camera Cameras;
    public Light moonLight;
    public float lightIntensity = 2;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyUp (KeyCode.Space)) 
		{
			gameObject.transform.eulerAngles = new Vector3 (0, 0, 0);
			Cameras.depth = 12;
            moonLight.intensity = lightIntensity;
		}
		if (Input.GetKey (KeyCode.UpArrow)) 
		{
			gameObject.transform.Translate (Vector3.forward/15);
		}	
		if (Input.GetKey (KeyCode.RightArrow)) 
		{
			gameObject.transform.Translate (Vector3.right/15);
		}	
		if (Input.GetKey (KeyCode.LeftArrow)) 
		{
			gameObject.transform.Translate (Vector3.left/15);
		}	
		if (Input.GetKey (KeyCode.DownArrow)) 
		{
			gameObject.transform.Translate (Vector3.back/15);
		}	
		if (Input.mousePosition.x < Screen.width / 5) 
		{
			gameObject.transform.Rotate (Vector3.down);
		}
		if (Input.mousePosition.x > Screen.width*4 / 5) 
		{
			gameObject.transform.Rotate (Vector3.up);
		}
		if (Input.mousePosition.y < Screen.height / 5) 
		{
			gameObject.transform.Rotate (Vector3.right);
		}
		if (Input.mousePosition.y > Screen.height*4 / 5) 
		{
			gameObject.transform.Rotate (Vector3.left);
		}
	}
}
