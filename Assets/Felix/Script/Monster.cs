﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Monster : MonoBehaviour
{
    public int HP = 1;
    public Animator animator;
    public GameObject target;
    public NavMeshAgent agent;
    public Vector3[] patrolPath;
    public float attackRange = 0.5f;
    public float Speed = 1.5f;
    public float attackTime;
    public GameObject destroyObj;
    public Collider alert;

    private enum Type { NONE, Fight, PATROL, DONMOVE, RUNAWAY }
    private Type type;
    private int patrolPoint;
    private float maxAttackTime;

    private List<GameObject> targets = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        agent.autoBraking = false;
        maxAttackTime = attackTime;
    }

    public virtual void Update()
    {
        switch (type)
        {
            case Type.Fight:
                Fight();
                break;
            case Type.PATROL:
                StartPatrol();
                break;
            default:
                break;
        }
    }

    private void Move()
    {
        agent.isStopped = false;
        if (target != null)
        {
            agent.angularSpeed = 160;
            agent.speed = Speed * 1.5f;
            animator.SetBool("Walk", false);
            animator.SetBool("Run", true);
        }
        else
        {
            agent.angularSpeed = 120;
            agent.speed = Speed;
            animator.SetBool("Walk", true);
            animator.SetBool("Run", false);
        }
    }

    private void Stop()
    {
        agent.isStopped = true;
        animator.SetBool("Walk", false);
        animator.SetBool("Run", false);
    }

    private void Fight()
    {
        maxAttackTime += Time.deltaTime;
        if (target != null)
        {
            agent.SetDestination(target.transform.position);
            Debug.Log(agent.remainingDistance);
            // 如果能够得着了，就停下攻击
            if (agent.remainingDistance <= attackRange)
            {
                // 攻擊速度最短時間以動畫長度為主
                AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
                if ((maxAttackTime >= attackTime) && (!info.IsName("Attack")))
                {
                    Debug.Log("Attack...");
                    Stop();
                    animator.SetTrigger("Attack");
                    SetTarget();
                    maxAttackTime = 0;
                }
            }
            else
            {
                Move();
            }
        }
        else
        {
            Stop();
            maxAttackTime = attackTime;
            type = Type.NONE;
        }
    }

    public void Platrol()
    {
        Debug.Log("Platrol...");

        if (patrolPath.Length == 0)
        {
            return;
        }

        // 當巡邏一圈後會回到原來的點
        patrolPoint = (patrolPoint + 1) % patrolPath.Length;
        agent.SetDestination(patrolPath[patrolPoint]);
        type = Type.PATROL;
    }

    private void StartPatrol()
    {
        if (agent.remainingDistance <= 0.5f)
        {
            Stop();
            type = Type.NONE;
        }
        else
        {
            Move();
        }
    }

    private void CloseCollider()
    {

    }

    public virtual void GetDoFu()
    {

    }

    public virtual void GetLiBai()
    {

    }

    public virtual void GetWangWei()
    {
        Attacked();
    }

    private void Attacked()
    {
        HP = HP - 1;
        animator.SetTrigger("GetHit");
        animator.SetInteger("HP", HP);
    }

    public void CallDestroy()
    {
        Destroy(destroyObj, 5);
    }

    private void SetTarget()
    {
        if (targets != null)
        {
            Random.InitState(System.Guid.NewGuid().GetHashCode());
            int ran = Random.Range(1, targets.Count);
            target = targets[ran - 1];
        }
    }

    public void FightEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            targets.Add(other.gameObject);
            if (target == null)
            {
                Debug.Log("Fight...");
                target = other.gameObject;
                agent.SetDestination(target.transform.position);
                type = Type.Fight;
            }
        }
    }

    public void FightExit(Collider other)
    {
        if (other.tag == "Player")
        {
            targets.Remove(other.gameObject);
        }
    }

    public void OnTriggerEvent(Collider other)
    {
        Stop();
        if (other.tag == "Do_Fu")
        {
            GetDoFu();
        }
        else if (other.tag == "Li_Bai")
        {
            GetLiBai();
        }
        else if (other.tag == "Wang_Wei")
        {
            GetWangWei();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        OnTriggerEvent(other);
    }


}
