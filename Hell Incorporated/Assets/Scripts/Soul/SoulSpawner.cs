using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulSpawner : MonoBehaviour
{
    [SerializeField]
    private float m_spawnRate = 0;
    [SerializeField]
    private int m_queueAmmount = 0;
    [SerializeField]
    private float m_timeForNextQueue = 0;
    private float m_nextQueueTimer;
    private int m_inQueue;
    [SerializeField]
    private GameObjectPool m_soulPool = null;
    [SerializeField]
    private ProcessorListSet m_procListSet = null;
    private float m_timer;
    private Soul m_currentSoul;
    bool m_canSpawn;

    private void Start()
    {
    }

    private void Update()
    {
        //Check if theres any souls n the queue and spawn at the spawn rate
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

    public void StartSpawning()
    {
        //Enable spawning and initialize timer values
        m_canSpawn = true;
        m_timer = m_spawnRate;
        m_inQueue = m_queueAmmount;
        m_nextQueueTimer = m_timeForNextQueue;
    }

    public void StopSpawning()
    {
        m_canSpawn = false;
    }

    private void SpawnSoul()
    {
        if (!m_canSpawn)
            return;
        //Get a soul from the object pool and assign it as the current soul
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
