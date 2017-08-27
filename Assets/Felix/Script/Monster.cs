using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Monster : MonoBehaviour
{
    public Animator animator;
    public GameObject target;
    public NavMeshAgent agent;
    public Vector3[] patrolPath;
    public float attackRange = 0.5f;
    public float speed;

    private bool isPatrol;
    private int patrolPoint = 1;

    // Use this for initialization
    void Start()
    {
        agent.autoBraking = false;
    }

    public void GoToNextPoint()
    {
        if (patrolPath.Length == 0)
        {
            return;
        }
        animator.SetBool("Walk", true);
        agent.isStopped = false;
        agent.SetDestination(patrolPath[patrolPoint]);
        // 當巡邏一圈後會回到原來的點
        patrolPoint = (patrolPoint + 1) % patrolPath.Length;
        isPatrol = true;
        //agent.SetDestination(target.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            //如果能够得着了，就停下攻击
            if (agent.remainingDistance <= attackRange)
            {
                agent.isStopped = true;
                animator.SetBool("Attack", true);
            }
        }
        else if (isPatrol)
        {

            if (agent.remainingDistance <= 0.5f)
            {
                agent.isStopped = true;
                animator.SetBool("Walk", false);
                isPatrol = false;
            }
        }
    }
}
