using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProcessor : Processor
{
    [SerializeField]
    private FloatValue m_soulQuota;
    [SerializeField]
    private float m_initSoulQuota;
    [SerializeField]
    private ProcState m_procState;
    [SerializeField]
    private GameEvent m_endGameEvent;

    private void Start()
    {
        m_soulQuota.value = m_initSoulQuota;
    }

    private void Update()
    {
        if (m_soulQuota.value <= 0)
        {
            m_endGameEvent.Invoke();
            m_procState.state = ProcState.ProcessorState.None;
        }
    }

    public void Restart()
    {
        m_soulQuota.value = m_initSoulQuota;
        m_procListSet.Add(this);
        m_procState.state = ProcState.ProcessorState.Scan;
    }

    public void DisableProcessor()
    {
        m_procListSet.Remove(this);
        base.SendToHell();
    }

    public override void StartProcessing(Soul soul)
    {
        base.StartProcessing(soul);
        m_procState.state = ProcState.ProcessorState.Scan;
    }

    public void InitPlayer()
    {
        Init();
        m_soulQuota.value = m_initSoulQuota;
    }

    public override void SendToHell()
    {
        base.SendToHell();
        m_soulQuota.value -= 1;
    }
}
