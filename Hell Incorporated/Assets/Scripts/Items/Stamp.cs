using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamp : MonoBehaviour
{
    [SerializeField]
    private FloatEvent m_stampEvent = null;
    [SerializeField]
    private ProcState m_procState = null;
    [SerializeField]
    private float m_rotationSpeed = 0;
    private bool m_autoStamp;

    public void ToggleAutoStamp()
    {
        m_autoStamp = !m_autoStamp;
    }

    public void StampPaper()
    {
        m_stampEvent.Invoke(0.0f);
        m_procState.state = ProcState.ProcessorState.Scan;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (m_autoStamp)
            return;
        if (m_procState.state != ProcState.ProcessorState.Stamp)
            return;
        if (other.transform.CompareTag("StampArea"))
        {
            StampPaper();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("Paper"))
        {
            Quaternion q = Quaternion.Euler(0, 0, 0);
            transform.parent.rotation = Quaternion.RotateTowards(transform.parent.rotation, q, m_rotationSpeed);
        }
    }
}
