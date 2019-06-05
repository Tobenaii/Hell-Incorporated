using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeWorker : Worker
{
    [SerializeField]
    private Computer m_computer = null;
    private enum TypePhase { Wait, Type };
    private TypePhase m_typePhase = TypePhase.Wait;
    private Vector3 pos1;
    private Vector3 pos2;

    protected override void DoAction()
    {
        if (m_typePhase == TypePhase.Wait)
        {
            if (MoveToPosition(transform, pos1, m_workSpeed))
            {
                m_startedState = false;
                m_typePhase = TypePhase.Type;
            }
        }

        else if (m_typePhase == TypePhase.Type)
        {
            m_startedState = true;
            if (MoveToPosition(transform, pos2, m_workSpeed))
            {
                m_typePhase = TypePhase.Wait;
                m_computer.Type();
            }
        }
    }

    protected override void InitWorker()
    {
        pos1 = transform.position;
        pos2 = transform.position + transform.up * -0.3f;
    }
}
