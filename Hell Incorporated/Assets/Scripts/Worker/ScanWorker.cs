using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanWorker : Worker
{
    private enum ScanPhase { Wait, Scan};

    [SerializeField]
    private GameObject m_scanner = null;
    private ScanPhase m_scanPhase = ScanPhase.Wait;
    private Vector3 pos1;
    private Vector3 pos2;

    protected override void DoAction()
    {
        if (m_scanPhase == ScanPhase.Wait)
        {
            if (MoveToPosition(m_scanner.transform, pos1, m_workSpeed))
            {
                m_scanner.transform.rotation = Quaternion.Euler(0, -90, 90);
                m_startedState = false;
                m_scanPhase = ScanPhase.Scan;
            }
        }
        else if (m_scanPhase == ScanPhase.Scan)
        {
            if (!m_processor.HasHaper)
                return;
            m_startedState = true;
            if (MoveToPosition(m_scanner.transform, pos2, m_workSpeed))
            {
                m_scanPhase = ScanPhase.Wait;
                m_scanner.GetComponent<Scanner>().Scan();
            }
        }
    }

    protected override void InitWorker()
    {
        m_scanner.GetComponent<Scanner>().SetAutoScan();
        m_scanner.GetComponent<Rigidbody>().isKinematic = true;
        pos1 = m_imp.transform.position + m_imp.transform.right * -0.3f;
        pos2 = m_imp.transform.position + m_imp.transform.right * -0.5f;
    }
}
