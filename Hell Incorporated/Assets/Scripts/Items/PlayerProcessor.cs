using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProcessor : Processor
{
    [SerializeField]
    private FloatEvent m_scoreEvent = null;

    public override void SendToHell()
    {
        base.SendToHell();
        m_scoreEvent.Invoke(1.0f);
    }
}
