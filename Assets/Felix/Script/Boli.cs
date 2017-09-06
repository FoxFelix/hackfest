using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boli : Monster
{
    public Transform[] rebirthPoint;
    public SkinnedMeshRenderer skim;
    public override void Update()
    {
        if (target != null)
        {
            float distance = Vector3.Distance(target.transform.position, transform.position);
            float look = 1.0f - Mathf.Clamp((distance / 6f), 0.1f, 1f);
            skim.material.color = new Color(1, 1, 1, look);
        }
        if (PhotonNetwork.isMasterClient)
        {
            base.Update();
        }
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
        /* Debug.Log("DeBuff in DoFu");
        StopCoroutine("Display");
        StartCoroutine("Display"); */
        ParticleManager.GenerateParticle(effect[0], transform);
        base.GetDoFu();
    }

    /* private IEnumerator Display()
    {
        // 現形
        yield return new WaitForSeconds(2);
        // 隱形
    } */

    public override void GetLiBai()
    {
        /* Debug.Log("DeBuff in Li_Bai");
        StopCoroutine("FixedBody");
        animator.SetBool("FixedBody", true);
        StartCoroutine("FixedBody"); */
        ParticleManager.GenerateParticle(effect[1], transform);
        base.GetDoFu();
    }

    /* private IEnumerator FixedBody()
    {
        yield return new WaitForSeconds(3);
        animator.SetBool("FixedBody", false);
    } */

    public override void GetWangWei()
    {
        /* å */
        ParticleManager.GenerateParticle(effect[0], transform);
        base.GetDoFu();
    }
}
