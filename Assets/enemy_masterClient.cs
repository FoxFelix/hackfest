using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_masterClient : MonoBehaviour {
    public Boss bossScript;
    public Boli boliScript;
	// Use this for initialization
	void Start () {
		if (!PhotonNetwork.isMasterClient)
        {
            if (bossScript != null)
                bossScript.enabled = false;
            if (boliScript != null)
                boliScript.enabled = false;

        }
	}
}
