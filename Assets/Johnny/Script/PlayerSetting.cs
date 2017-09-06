using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetting : MonoBehaviour {
	public GameObject NoHeadModel;
    public GameObject FullModel;
	public Vector3 StartPos;

	void Start () 
	{
		gameObject.transform.position = StartPos;
		if (GetComponent<PhotonView> ().isMine) 
		{
            FullModel.SetActive (false);
            NoHeadModel.SetActive(true);
        }
        if (!GetComponent<PhotonView>().isMine)
        {
            FullModel.SetActive(true);
            NoHeadModel.SetActive(false);
        }
    }

}
