using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulSpawner : MonoBehaviour
{
    [SerializeField]
    private float m_spawnRate;
    [SerializeField]
    private int m_queueAmmount;
    [SerializeField]
    private float m_timeForNextQueue;
    private float m_nextQueueTimer;
    private int m_inQueue;
    [SerializeField]
    private GameObjectPool m_soulPool;
    [SerializeField]
    private ProcessorListSet m_procListSet;
    private float m_timer;
    private Soul m_currentSoul;

    private void Start()
    {
        m_timer = m_spawnRate;
        m_inQueue = m_queueAmmount;
        m_nextQueueTimer = m_timeForNextQueue;
    }

    private void Update()
    {
        if (m_inQueue == 0)
        {
            m_nextQueueTimer -= Time.deltaTime;
            if (m_nextQueueTimer <= 0)
            {
                m_nextQueueTimer = m_timeForNextQueue;
                m_inQueue = m_queueAmmount;
            }
            return;
        }
        m_timer -= Time.deltaTime;
        if (m_timer <= 0)
        {
            m_timer = m_spawnRate;
            SpawnSoul();
        }
    }

    private void SpawnSoul()
    {
        if (m_currentSoul == null)
        {
            GameObject soulObj = m_soulPool.GetObject();
            soulObj.transform.position = transform.position;
            Soul soul = soulObj.GetComponent<Soul>();
            m_currentSoul = soul;
        }
        else if (!m_currentSoul.FindProcessor(m_procListSet))
            m_timer = 0.1f;
        else
        {
            m_currentSoul = null;
            m_inQueue--;
        }
    }
}
