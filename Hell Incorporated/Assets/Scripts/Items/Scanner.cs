using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : Item
{
    [SerializeField]
    private float m_rotationSpeed = 0;
    [SerializeField]
    private AudioSource m_scanAudio;

    private void Start()
    {
        m_procState.state = ProcState.ProcessorState.Scan;
    }

    private void OnTriggerStay(Collider other)
    {
        if (m_procState.state != ProcState.ProcessorState.Scan)
            return;
        if (other.transform.CompareTag("Paper"))
        {
            Quaternion q = Quaternion.Euler(180, 0, -90);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, m_rotationSpeed);
            if (transform.rotation == q)
                DoAction();
        }
    }

    public override void DoAction()
    {
        m_actionEvent.Invoke();
        m_procState.state = ProcState.ProcessorState.Type;
        m_scanAudio.Play();
        if (m_inTutorial)
        {
            m_arrow.SetActive(false);
            m_tutorialDoneEvent.Invoke();
            m_inTutorial = false;
        }
    }
}
