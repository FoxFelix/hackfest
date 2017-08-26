using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fxManager : MonoBehaviour {

    PhotonView pv;
    public GameObject liBaiAttackPrefab;
    public Transform bulletSpawnPoint;


	// Use this for initialization
	void Start () {
        GetComponent<PhotonView>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    [PunRPC]
    void fxAttackLiBai()
    {
        Instantiate(liBaiAttackPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        print("attack");
    }
}
