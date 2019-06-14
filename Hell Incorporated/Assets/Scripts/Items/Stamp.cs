using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamp : Item
{
    [SerializeField]
    private GameEvent m_stampEvent = null;
    [SerializeField]
    private float m_rotationSpeed = 0;
    [SerializeField]
    private AudioSource m_stampAudio;
    [SerializeField]
    private GameEvent m_tutorialEvent;
    [SerializeField]
    private GameEvent m_startGameEvent;
    private bool m_gameStarted;
    private bool m_autoStamp;

    public void ToggleAutoStamp()
    {
        m_autoStamp = !m_autoStamp;
    }

    private void Update()
    {
        if (m_tut)
        {
            if (m_procState.state == ProcState.ProcessorState.Stamp)
            {
                m_arrow.SetActive(true);
                m_tut = false;
            }
        }
    }

    public void StampPaper()
    {
        m_stampEvent.Invoke();
        m_procState.state = ProcState.ProcessorState.Scan;
        m_stampAudio.Play();
        m_arrow.SetActive(false);
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
            if (!m_gameStarted)
            {
                m_startGameEvent.Invoke();
                m_gameStarted = true;
            }
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
