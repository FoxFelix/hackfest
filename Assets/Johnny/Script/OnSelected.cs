using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnSelected : Photon.MonoBehaviour {
	public bool lee;
	public bool wong;
	public bool du;
	public GameObject leePic;
	public GameObject wongPic;
	public GameObject duPic;
	public GameObject leeCharacter;
	public GameObject wongCharacter;
	public GameObject duCharacter;
	public GameObject StartPoint;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (lee) 
		{
			leePic.SetActive (false);
		}
		if (wong) 
		{
			wongPic.SetActive (false);
		}
		if (du) 
		{
			duPic.SetActive (false);
		}
	}
	[PunRPC]
	public void selectLee(bool open)
	{
		leePic.transform.localPosition = new Vector3 (leePic.transform.localPosition.x, -1000, leePic.transform.localPosition.z);
		leeCharacter.transform.localPosition = new Vector3 (leeCharacter.transform.localPosition.x, -1000, leeCharacter.transform.localPosition.z);
	}
	[PunRPC]
	public void selectWong(bool open)
	{
		wongPic.transform.localPosition = new Vector3 (wongPic.transform.localPosition.x, -1000, wongPic.transform.localPosition.z);
		wongCharacter.transform.localPosition = new Vector3 (wongCharacter.transform.localPosition.x, -1000, wongCharacter.transform.localPosition.z);
	}
	[PunRPC]
	public void selectDu(bool open)
	{
		duPic.transform.localPosition = new Vector3 (duPic.transform.localPosition.x, -1000, duPic.transform.localPosition.z);
		duCharacter.transform.localPosition = new Vector3 (duCharacter.transform.localPosition.x, -1000, duCharacter.transform.localPosition.z);
	}
}
