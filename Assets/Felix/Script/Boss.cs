using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Flags]
public enum DeBuff
{
    NONE = 0,
    DOFU = 1,
    LIBAI = 2,
    WANGWEI = 4,
    BREAKOUT = 7
}
public class Boss : Monster
{
    public DeBuff deBuff;
    public override void Update()
    {
        Collider body = GetComponent<Collider>();
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        if ((info.IsName("GetHit")) || (info.IsName("Shout")) || (info.IsName("Death1")))
        {
            body.enabled = false;
            alert.enabled = false;
            return;
        }
        else
        {
            body.enabled = true;
            alert.enabled = true;
        }
        base.Update();
    }

    public void Push()
    {
        target.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * 500);
    }
    public override void GetDoFu()
    {
        Debug.Log("DeBuff in DoFu");
        if ((deBuff == DeBuff.NONE) || (deBuff == DeBuff.DOFU))
        {
            deBuff |= DeBuff.DOFU;
            Debug.Log("播放「弱」-成功");
        }
        else
        {
            Debug.Log("播放「弱」-失敗");
            deBuff = DeBuff.NONE;
        }
    }

    public override void GetLiBai()
    {
        Debug.Log("DeBuff in LiBai");
        if (deBuff == DeBuff.DOFU)
        {
            deBuff |= DeBuff.LIBAI;
            Debug.Log("播放「縛」-成功");
        }
        else
        {
            Debug.Log("播放「縛」-失敗");
            deBuff = DeBuff.NONE;
        }
    }

    public override void GetWangWei()
    {
        Debug.Log("DeBuff in WangWei");
        deBuff |= DeBuff.DOFU;
        deBuff |= DeBuff.LIBAI;
        if (deBuff == DeBuff.BREAKOUT)
        {
            base.GetWangWei();
            deBuff = DeBuff.NONE;
        }
        else if (deBuff == (DeBuff.DOFU | DeBuff.LIBAI))
        {
            deBuff |= DeBuff.WANGWEI;
            Debug.Log("播放「收」-成功");
        }
        else
        {
            Debug.Log("播放「收」-失敗");
            deBuff = DeBuff.NONE;
        }
    }
}
