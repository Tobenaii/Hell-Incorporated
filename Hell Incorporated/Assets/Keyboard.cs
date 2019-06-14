using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboard : Item
{
    private void Update()
    {
        if (m_tut)
        {
            if (m_procState.state == ProcState.ProcessorState.Type)
            {
                m_arrow.SetActive(true);
                m_tut = false;
            }
        }
    }

    public void DoneType()
    {
        m_arrow.SetActive(false);
    }
}
