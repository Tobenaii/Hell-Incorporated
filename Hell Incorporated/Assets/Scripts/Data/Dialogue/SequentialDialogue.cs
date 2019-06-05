using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequentialDialogue : Dialogue
{
    [SerializeField]
    private DialogueList m_dialogueList;
    [SerializeField]
    private List<int> m_dialogueIds = null;
    private int m_curDialogue = -1;

    public void StartNextDialogue()
    {
        m_curDialogue++;
        if (m_curDialogue == m_dialogueList.Count)
            m_curDialogue = 0;
        string dialogue = m_dialogueList.GetDialogue(m_dialogueIds[m_curDialogue]);
        StartDialogue(dialogue);
    }
}
