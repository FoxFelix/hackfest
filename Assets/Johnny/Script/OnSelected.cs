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
		leePic.SetActive (open);
	}
	[PunRPC]
	public void selectWong(bool open)
	{
		wongPic.SetActive (open);
	}
	[PunRPC]
	public void selectDu(bool open)
	{
		duPic.SetActive (open);
	}
}
