using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour {
	public GameObject hand;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () 
	{
		hand = GameObject.Find ("LiBaiEffect");
		GetComponent<LineRenderer> ().SetPosition (0, gameObject.transform.position);
		GetComponent<LineRenderer> ().SetPosition (1, hand.transform.position);
	}
}
