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
        //Do the scanner action if the current state is Scan and the trigger is paper
        if (m_procState.state != ProcState.ProcessorState.Scan)
            return;
        if (other.transform.CompareTag("Paper"))
        {
            DoAction();
        }
    }

    public override void DoAction()
    {
        //Invoke the scanner event
        m_actionEvent.Invoke();
        m_procState.state = ProcState.ProcessorState.Type;
        m_scanAudio.Play();
        //If we're in the tutorial, invoke scan tutorial event aswel and disable the tutorial arrow
        if (m_inTutorial)
        {
            m_arrow.SetActive(false);
            m_tutorialDoneEvent.Invoke();
            m_inTutorial = false;
        }
    }
}
