using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequentialDialogue : Dialogue
{
    [SerializeField]
    private List<DialogueList> m_dialogueList;

    private int m_curDialogueList = -1;
    private int m_curDialogue = -1;

    public void StartNextDialogue()
    {
        if (m_curDialogueList == -1)
            return;
        m_curDialogue++;
        if (m_curDialogue >= m_dialogueList[m_curDialogueList].Count)
            return;
        DialogueThingy dialogue = m_dialogueList[m_curDialogueList].GetDialogue(m_curDialogue);
        StartDialogue(dialogue);
        return ;
    }

    public void OpenNextDialogue()
    {
        m_dialogueBox.SetActive(true);
        GetComponent<BoxCollider>().enabled = true;
        m_curDialogue = -1;
        m_curDialogueList++;
        if (m_curDialogueList >= m_dialogueList.Count)
            return;
        StartNextDialogue();
    }
}
