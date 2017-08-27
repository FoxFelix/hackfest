using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class test : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject Obj;
    // Use this for initialization
    void Start()
    {
        agent.SetDestination(Obj.transform.position);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
