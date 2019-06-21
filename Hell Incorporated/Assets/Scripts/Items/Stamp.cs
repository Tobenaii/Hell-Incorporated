using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamp : Item
{
    [SerializeField]
    private float m_rotationSpeed = 0;
    [SerializeField]
    private AudioSource m_stampAudio;
    [SerializeField]
    private GameEvent m_tutorialEvent;

    public override void DoAction()
    {
        //Invoke stamp event
        m_actionEvent.Invoke();
        m_procState.state = ProcState.ProcessorState.None;
        m_stampAudio.Play();
        //If we're in the tutorial, invoke tutorial event and disable arrow
        if (m_inTutorial)
        {
            m_arrow.SetActive(false);
            m_tutorialDoneEvent.Invoke();
            m_inTutorial = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Do the stamp action if the current state is Stamp and the trigger is paper
        if (m_procState.state != ProcState.ProcessorState.Stamp)
            return;
        if (other.transform.CompareTag("StampArea"))
        {
            DoAction();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //Rotate the stamp towards the paper so it can stamp properly
        if (other.transform.CompareTag("Paper"))
        {
            Quaternion q = Quaternion.Euler(0, 0, 0);
            transform.parent.rotation = Quaternion.RotateTowards(transform.parent.rotation, q, m_rotationSpeed);
        }
    }
}
