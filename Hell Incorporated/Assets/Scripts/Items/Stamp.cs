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
    [SerializeField]
    private AudioSource m_stampAudio;
    [SerializeField]
    private GameEvent m_tutorialEvent;
    private bool m_autoStamp;

    public void ToggleAutoStamp()
    {
        m_autoStamp = !m_autoStamp;
    }

    public void StampPaper()
    {
        m_stampEvent.Invoke(0.0f);
        m_procState.state = ProcState.ProcessorState.Scan;
        m_stampAudio.Play();
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
            m_tutorialEvent.Invoke();
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
