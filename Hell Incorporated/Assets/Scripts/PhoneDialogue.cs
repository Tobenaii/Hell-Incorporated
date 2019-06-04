using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneDialogue : MonoBehaviour
{
    [SerializeField]
    [TextArea]
    private string m_dialogue1;
    [SerializeField]
    private StringEvent m_dialogueEvent;

    private void Start()
    {
        m_dialogueEvent.Invoke(m_dialogue1);
    }
}
