using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headColliderUpdate : MonoBehaviour {

    SphereCollider sphere;
    public Transform headBone;

	void Start () {
        sphere = GetComponent<SphereCollider>();
	}
	
	// Update is called once per frame
	void Update () {
        sphere.center = headBone.localPosition - new Vector3(0, 0.1f, 0);
	}
}
