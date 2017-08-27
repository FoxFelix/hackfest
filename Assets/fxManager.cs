using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fxManager : MonoBehaviour {

    PhotonView pv;
    public GameObject liBaiAttackPrefab;
    public Transform bulletSpawnPoint;
    public PsUnit liBaiAttackEffect;


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
        ParticleManager.GenerateParticle(liBaiAttackEffect, bulletSpawnPoint);
        print("attack");
    }

    [PunRPC]
    public void fxThrow(float force)
    {
        GameObject thrownObject = GameObject.Instantiate(liBaiAttackPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        var rb = thrownObject.GetComponent<Rigidbody>();
        rb.AddForce(bulletSpawnPoint.forward * force, ForceMode.Impulse);
        print("throw");
    }
}
