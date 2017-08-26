using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bodyDirectionSync : MonoBehaviour {
    public Transform cam;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Quaternion camRot = cam.transform.localRotation;
        camRot.x = 0;
        camRot.z = 0;
        this.transform.localRotation = camRot;
    }
}
