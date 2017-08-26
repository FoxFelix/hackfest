using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarAnimator : MonoBehaviour {

    public Rigidbody rb;
    public Animator anim;
    public Transform direction;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        var forwardCheck = Vector3.Dot(rb.velocity.normalized, direction.forward.normalized);
        //var rightCheck = Vector3.Dot(rb.velocity.normalized, direction.right.normalized);
		if (rb.velocity.magnitude >= 0.3f)
        {
            if (forwardCheck > 0.5f)
                anim.SetFloat("forward", 1);
            //if (rightCheck > 0.5f)
            //    anim.SetFloat("right", 1);
            //if (rightCheck < -0.5f)
            //    anim.SetFloat("left", 1);
        }
        if (rb.velocity.magnitude < 0.2f)
        {
                anim.SetFloat("forward", 0);
                //anim.SetFloat("right", 0);
                //anim.SetFloat("left", 0);
        }
    }
}
