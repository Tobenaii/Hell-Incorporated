using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : Interactable
{
    [SerializeField]
    private AudioSource m_phoneRingSound;
    [SerializeField]
    private Dialogue m_dialogue;
    private DialogueData m_currentDialogueData;
    [SerializeField]
    private Animator m_animator;

    public void Ring(DialogueData dList)
    {
        m_phoneRingSound.Play();
        m_currentDialogueData = dList;
        m_animator.SetTrigger("Vibrate");
    }

    public override void OnClick(GameObject hand)
    {
        if (!m_currentDialogueData)
            return;
        base.OnClick(hand);
        OpenDialogue(m_currentDialogueData);
        m_currentDialogueData = null;
    }

    public void OpenDialogue(DialogueData data)
    {
        m_phoneRingSound.Stop();
        m_animator.SetTrigger("Stop");
        m_dialogue.StartDialogue(data);
        m_currentDialogueData = null;
    }
}
