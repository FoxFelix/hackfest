using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyNetworkSync : MonoBehaviour {
    NavMeshAgent agent;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        InvokeRepeating("checkIfMasterClient", 5, 5);

    }
	

    void checkIfMasterClient()
    {
        if (PhotonNetwork.isMasterClient)
            agent.enabled = true;
        if (!PhotonNetwork.isMasterClient)
            agent.enabled = false;
    }
}
