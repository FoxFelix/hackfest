using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DD_cameraFollow : MonoBehaviour {
    public Transform planeBase;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = planeBase.transform.position;
        var newRot = planeBase.transform.rotation;
        newRot.x = 0;
        newRot.z = 0;
        transform.rotation = newRot;
	}
}
