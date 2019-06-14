using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequentialDialogue : Dialogue
{
    [SerializeField]
    private List<DialogueList> m_dialogueList;
    [SerializeField]
    private AudioSource m_phoneRing;

    private int m_curDialogueList = -1;
    private int m_curDialogue = -1;

    public void StartNextDialogue()
    {
        if (m_phoneRing != null)
            m_phoneRing.Stop();
        if (m_curDialogueList == -1)
            OpenNextDialogue();
        m_curDialogue++;
        if (m_curDialogue >= m_dialogueList[m_curDialogueList].Count)
            return;
        m_dialogueBox.SetActive(true);
        GetComponent<BoxCollider>().enabled = true;
        DialogueThingy dialogue = m_dialogueList[m_curDialogueList].GetDialogue(m_curDialogue);
        StartDialogue(dialogue);
        return;
    }

    public void OpenNextDialogue()
    {
        m_curDialogue = -1;
        m_curDialogueList++;
        if (m_curDialogueList >= m_dialogueList.Count)
            return;
        StartNextDialogue();
    }

    public void QueueNextDialogue()
    {
        m_curDialogue++;
    }
}
