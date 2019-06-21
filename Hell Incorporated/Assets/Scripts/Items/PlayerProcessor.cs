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

    private bool m_ended;

    private void Start()
    {
        m_soulQuota.value = m_initSoulQuota;
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (m_ended)
            return;
        if (m_soulQuota.value <= 0)
        {
            m_procState.state = ProcState.ProcessorState.None;
            m_ended = true;
            m_endGameEvent.Invoke();
        }
    }

    public void Restart()
    {
        m_soulQuota.value = m_initSoulQuota;
        m_procListSet.Add(this);
        m_procState.state = ProcState.ProcessorState.Scan;
        m_ended = false;
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
