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

    private void Start()
    {
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
