using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour 
{
    private static readonly Dictionary<string, Queue<PsUnit>> m_PU_Pool = new Dictionary<string, Queue<PsUnit>>();
    private static readonly Dictionary<string, PsUnit> m_ParticleMemory = new Dictionary<string, PsUnit>();
    private static readonly Dictionary<string, int> m_DicCount = new Dictionary<string, int>();
    private static int LimitParticle = 50;

    private void Awake()
    {
    }

    public static void GenerateParticle(PsUnit effectObj, Transform player)
    {
        PsUnit objFromPool = GetObjFromPool(effectObj);

        if (objFromPool != null)
        {
            //Set pos & rotate
            objFromPool.transform.position = player.position;
            objFromPool.transform.rotation = player.rotation;

            //Set ExistTime
            objFromPool.SetLifeTime(3); //預設存在3秒

            //Show Effect
            objFromPool.gameObject.SetActive(true);
            objFromPool.ActiveParticleSystem();
        }
    }

    private static PsUnit GetObjFromPool(PsUnit effectObj)
    {
        PsUnit particleUnit = null;
        if (m_PU_Pool.ContainsKey(effectObj.name))
        {
            Queue<PsUnit> queue = m_PU_Pool[effectObj.name];
            if (queue.Count > 0)
            {
                particleUnit = queue.Dequeue();
            }
        }
        if (particleUnit == null)
        {
            if (m_ParticleMemory.ContainsKey(effectObj.name) && m_DicCount[effectObj.name] > LimitParticle)
            {
                particleUnit = m_ParticleMemory[effectObj.name];
            }
            else
            {
                particleUnit = CreateParticleObj(effectObj);
            }
        }
        return particleUnit;
    }

    private static PsUnit CreateParticleObj(PsUnit effectObj)
    {
        PsUnit psUnit = UnityEngine.Object.Instantiate<PsUnit>(effectObj);
        psUnit.name = effectObj.name;
        psUnit.gameObject.SetActive(false);
        // Add to pool
        if (!m_ParticleMemory.ContainsKey(effectObj.name))
        {
            m_ParticleMemory.Add(effectObj.name, psUnit);
            m_DicCount.Add(effectObj.name, 0);
        }
        if (m_DicCount[effectObj.name] <= LimitParticle)
        {
            m_DicCount[effectObj.name] = m_DicCount[effectObj.name] + 1;
        }

        return psUnit;
    }

    public static void Recover(PsUnit effectObj)
    {
        effectObj.gameObject.SetActive(false);
        Queue<PsUnit> queue;
        if (!m_PU_Pool.TryGetValue(effectObj.name, out queue))
        {
            m_PU_Pool.Add(effectObj.name, queue = new Queue<PsUnit>());
        }
        queue.Enqueue(effectObj);
    }
}
