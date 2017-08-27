using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Monster : MonoBehaviour
{
    public int HP;
    public Animator animator;
    public GameObject target;
    public NavMeshAgent agent;
    public Vector3[] patrolPath;
    public float attackRange = 0.5f;
    public float Speed = 1.5f;
    public float attackTime;
    public Collider alert;
    public GameObject Detath;

    private enum Types { NONE, ATTACK, PATROL, DONMOVE, RUNAWAY }
    private Types type;
    private int patrolPoint = 1;
    private float time;
    private float donMoveTime = 1.5f;

    // Use this for initialization
    void Start()
    {
        //agent.autoBraking = false;
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
    }

    public void Attack()
    {
        agent.SetDestination(target.transform.position);
        agent.speed = Speed * 1.5f;
        type = Types.ATTACK;
    }
    private void CheckAttack()
    {
        if (target != null)
        {
            Debug.Log(agent.remainingDistance);
            //如果能够得着了，就停下攻击
            if (agent.remainingDistance <= attackRange)
            {
                Debug.Log("nono!!");
                if (time >= attackTime)
                {
                    agent.isStopped = true;
                    animator.SetBool("Run", false);
                    animator.SetTrigger("Attack");
                    time = 0;
                }
                time += Time.deltaTime;
            }
            else
            {
                agent.SetDestination(target.transform.position);
                agent.isStopped = false;
                animator.SetBool("Run", true);
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

    void OnTriggerEnter(Collider other)
    {
        target = other.gameObject;
        if (other.tag == "Magic")
        {
            agent.isStopped = true;
            animator.SetTrigger("Shout");
            type = Types.DONMOVE;
            time = 0;
        }
        else if (other.tag == "Weaton")
        {
            animator.SetTrigger("GetHit");
            HP -= 1;
            if (HP == 0)
            {
                animator.SetTrigger("Death1");
                Destroy(Detath, 5f);
            }
        }

    }
}
