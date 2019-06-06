using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampWorker : Worker
{
    [SerializeField]
    private GameObject m_stamp = null;
    private enum StampPhase { Wait, Stamp };
    private StampPhase m_stampPhase = StampPhase.Wait;
    private Vector3 pos1;
    private Vector3 pos2;

    protected override void DoAction()
    {
        if (m_stampPhase == StampPhase.Wait)
        {
            if (MoveToPosition(m_stamp.transform, pos1, m_workSpeed))
            {
                m_stamp.transform.rotation = Quaternion.identity;
                m_stampPhase = StampPhase.Stamp;
                m_startedState = false;
            }
        }
        else if (m_stampPhase == StampPhase.Stamp)
        {
            m_startedState = true;
            if (MoveToPosition(m_stamp.transform, pos2, m_workSpeed))
            {
                m_stampPhase = StampPhase.Wait;
                m_stamp.transform.GetChild(0).GetComponent<Stamp>().StampPaper();
            }
        }
    }

    protected override void InitWorker()
    {
        m_stamp.GetComponent<Rigidbody>().isKinematic = true;
        m_stamp.transform.GetChild(0).GetComponent<Stamp>().ToggleAutoStamp();
        pos1 = m_imp.transform.position + m_imp.transform.right * 0.4f;
        pos2 = (m_imp.transform.position + m_imp.transform.right * 0.4f) + Vector3.down * 0.3f;
    }

    protected override void Cleanup()
    {
        m_stamp.transform.GetChild(0).GetComponent<Stamp>().ToggleAutoStamp();
        m_stamp.GetComponent<Rigidbody>().isKinematic = false;
    }
}
