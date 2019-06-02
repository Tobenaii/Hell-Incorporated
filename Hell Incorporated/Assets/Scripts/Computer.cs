using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Computer : MonoBehaviour
{
    [SerializeField]
    private Text m_screenText;
    [SerializeField]
    private ProcState m_procState;
    int m_typeIndex;

    public void ResetWindow()
    {
        m_screenText.text = "";
        m_typeIndex = 0;
    }

    public void StartScreen()
    {
        m_screenText.text = "John Snow";
    }

    public void Type()
    {
        if (m_procState.state != ProcState.ProcessorState.Type)
            return;
        if (m_typeIndex == 5)
            m_procState.state = ProcState.ProcessorState.Stamp;
        m_screenText.text += "\n Nah Yeh Nah";
        m_typeIndex++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
