using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Keyboard : Item
{
    [SerializeField]
    private TextMeshPro m_text;
    [SerializeField]
    private AudioSource m_audio;
    private int m_typeIndex;

    public void ScannedNameData()
    {
        m_text.text = "John Snow";
    }

    public void DoneProcessing()
    {
        m_text.text = "";
    }

    public void Restart()
    {
        m_text.text = "";
    }

    public void DoneType()
    {
        //Change the processing state to stamp and invoke the action
        m_procState.state = ProcState.ProcessorState.Stamp;
        m_actionEvent.Invoke();
        if (m_inTutorial)
        {
            //If in tutorial, invoke keyboard tutorial done action
            m_inTutorial = false;
            m_arrow.SetActive(false);
            m_tutorialDoneEvent.Invoke();
        }
        m_typeIndex = 0;
    }

    public override void DoAction()
    {
        //Play type sound and update computer text
        m_audio.Play();
        if (m_procState.state != ProcState.ProcessorState.Type)
            return;
        if (m_typeIndex == 5)
            DoneType();
        m_text.text += "\n Yeh Nah";
        m_typeIndex++;
    }
}
