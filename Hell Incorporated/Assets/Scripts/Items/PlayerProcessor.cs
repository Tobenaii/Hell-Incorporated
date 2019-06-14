using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProcessor : Processor
{
    [SerializeField]
    private GameEvent m_processedEvent = null;
    [SerializeField]
    private FloatValue m_soulQuota;
    [SerializeField]
    private float m_initSoulQuota;

    private void Start()
    {
        
    }

    public void InitPlayer()
    {
        Init();
        m_soulQuota.value = m_initSoulQuota;
        m_processedEvent.Invoke();
    }

    public override void SendToHell()
    {
        base.SendToHell();
        m_soulQuota.value -= 1;
        m_processedEvent.Invoke();
    }
}
