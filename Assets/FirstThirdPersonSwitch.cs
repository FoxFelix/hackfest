using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstThirdPersonSwitch : MonoBehaviour {

    PhotonView pv;
    Rigidbody rb;
    public walkingScript walkingScript;
    public EasyInputVR.Misc.InstanceController instanceController;
    public EasyInputVR.StandardControllers.StandardLaserPointer standardLaserPointer;
    public Camera cam;
    public FlareLayer flare;
    public AudioListener audiolistner;
    public AvatarAnimator avatarAnim;

	// Use this for initialization
	void Awake () {
        pv = GetComponent<PhotonView>();


        if (pv.isMine)
        {
            cam.enabled = true; ;
        }
        if (!pv.isMine)
        {
            rb = GetComponent<Rigidbody>();
            Destroy(rb);
            standardLaserPointer.enabled = false;
            instanceController.enabled = false;
            walkingScript.enabled = false;
            cam.enabled = false;
            audiolistner.enabled = false;
            flare.enabled = false;
            avatarAnim.enabled = false;
            cam.tag = "Untagged";
            this.tag = "Team";
        }
	}
	
}
