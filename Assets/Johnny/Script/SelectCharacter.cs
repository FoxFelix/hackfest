using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyInputVR.Core;

public class SelectCharacter : MonoBehaviour {
	public string NowSelect;
	bool gasPressed;
	public GameObject[] Character;
	// Use this for initialization
	void OnEnable()
	{
		#if !UNITY_EDITOR && UNITY_ANDROID
		EasyInputHelper.On_Motion += localMotion;
		#endif
		EasyInputHelper.On_ClickStart += localClickStart;
		EasyInputHelper.On_ClickEnd += localClickEnd;

	}

	void OnDestroy()
	{
		#if !UNITY_EDITOR && UNITY_ANDROID
		EasyInputHelper.On_Motion -= localMotion;
		#endif
		EasyInputHelper.On_Click -= localClickStart;
		EasyInputHelper.On_Click -= localClickEnd;
	}
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{       
		if (Input.GetKeyUp (KeyCode.S)) 
		{
			gasPressed = true;
		}
	}
	void OnCollisionStay(Collision collision)
	{ 
		if (gasPressed == true) 
		{
			if (collision.transform.name == "lee") 
			{
				PhotonNetwork.Instantiate(this.Character[0].name, new Vector3(0f,0f,0f), Quaternion.identity, 0);
				Destroy (gameObject);
			}
			if (collision.transform.name == "wong") 
			{
				PhotonNetwork.Instantiate(this.Character[0].name, new Vector3(0f,0f,0f), Quaternion.identity, 0);
				Destroy (gameObject);
			}
			if (collision.transform.name == "du") 
			{
				PhotonNetwork.Instantiate(this.Character[0].name, new Vector3(0f,0f,0f), Quaternion.identity, 0);
				Destroy (gameObject);
			}
		}
	}
	void localClickStart(ButtonClick button)
	{
		//if (button.button == EasyInputConstants.CONTROLLER_BUTTON.GearVRTouchClick)
		//{
		//    brakePressed = true;
		//}
		if (button.button == EasyInputConstants.CONTROLLER_BUTTON.GearVRTouchClick)
		{
			gasPressed = true;
		}
	}

	void localClickEnd(ButtonClick button)
	{
		//if (button.button == EasyInputConstants.CONTROLLER_BUTTON.GearVRTouchClick)
		//{
		//    brakePressed = false;
		//}
		if (button.button == EasyInputConstants.CONTROLLER_BUTTON.GearVRTouchClick)
		{
			gasPressed = false;
		}
	}
}
