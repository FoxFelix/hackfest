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
    public float Speed;
    public float attackTime;


    private bool DoFuDeBuff;
    private bool LiBaiDeBuff;
    private bool WangWeiDeBuff;
    public enum Types { NONE, ATTACK, PATROL, DONMOVE, RUNAWAY }
    public Types type;
    private int patrolPoint = 1;
    private float time;
    private float donMoveTime = 1.5f;

    // Use this for initialization
    void Start()
    {
        agent.autoBraking = false;
        agent.isStopped = true;
        agent.speed = Speed;
        type = Types.NONE;
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
        type = Types.PATROL;
        //agent.SetDestination(target.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        switch (type)
        {
            case Types.ATTACK:
                CheckAttack();
                break;
            case Types.PATROL:
                CheckPatrol();
                break;
            case Types.DONMOVE:
                CheckNoMove();
                break;
        }

        CheckDeBuff();
    }

    private void CheckAttack()
    {
        if (target != null)
        {
            //如果能够得着了，就停下攻击
            if (agent.remainingDistance <= attackRange)
            {
                if (time >= attackTime)
                {
                    agent.isStopped = true;
                    animator.SetTrigger("Attack");
                    time = 0;
                }
                time += Time.deltaTime;
            }
            else
            {
                agent.speed = Speed * 1.5f;
                agent.isStopped = false;
                animator.SetBool("Run", true);
                agent.SetDestination(target.transform.position);
            }
        }
        else
        {
            agent.speed = Speed;
            agent.isStopped = true;
            animator.SetBool("Rum", false);
        }
    }

    private void CheckPatrol()
    {
        if (agent.remainingDistance <= 0.5f)
        {
            agent.isStopped = true;
            animator.SetBool("Walk", false);
            type = Types.NONE;
        }
    }

    public virtual void CheckNoMove()
    {
        if (time >= donMoveTime)
        {
            agent.isStopped = true;
        }

        time += Time.deltaTime;
    }

    public virtual void CheckDeBuff()
    {

    }

    public void GetHit()
    {
        animator.SetTrigger("GetHit");
    }
}
