using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boli : Monster
{
    public Transform[] rebirthPoint;
    public override void Update()
    {
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        if (info.IsName("BeAttacked"))
        {
            Debug.Log("BeAttacked...");
            Stop();
            return;
        }

        base.Update();
    }

    public override void Attack()
    {
        if (rebirthPoint.Length > 0)
        {
            Debug.Log("Runaway...");
            Vector3 runaway = target.transform.position;
            for (int i = 1; i <= rebirthPoint.Length; i++)
            {
                if (Vector3.Distance(target.transform.position, rebirthPoint[i % rebirthPoint.Length].position) > Vector3.Distance(target.transform.position, runaway))
                {
                    Debug.Log(i % rebirthPoint.Length);
                    runaway = rebirthPoint[i % rebirthPoint.Length].position;
                }
            }
            agent.SetDestination(runaway);
            type = Type.RUNAWAY;
        }
        else
        {
            Debug.LogError("未設定\"rebirthPoint \"");
        }
    }

    public override void GetDoFu()
    {
        Debug.Log("DeBuff in DoFu");
        StopCoroutine("Display");
        StartCoroutine("Display");
    }

    private IEnumerator Display()
    {
        // 現形
        yield return new WaitForSeconds(2);
        // 隱形
    }

    public override void GetLiBai()
    {
        Debug.Log("DeBuff in Li_Bai");
        StopCoroutine("FixedBody");
        animator.SetBool("FixedBody", true);
        StartCoroutine("FixedBody");
    }

    private IEnumerator FixedBody()
    {
        yield return new WaitForSeconds(3);
        animator.SetBool("FixedBody", false);
    }

    public override void GetWangWei()
    {
        Debug.Log("DeBuff in Wang_Wei");
        base.GetWangWei();
    }
}
