using UnityEngine;
using System.Collections;

public class PreventFromStopMoving : MonoBehaviour {

	Cloth c;
	float count;

	void Start () {
		c = transform.GetComponent<Cloth>();
		count = Random.Range(0f,3f);
	}
	
	// Update is called once per frame
	void Update () {
		c.useGravity = true;
		if(count >= 30) {
			c.useGravity = false;
			count = Random.Range(0,3f);
		}
		count += Time.deltaTime;
	}
}
