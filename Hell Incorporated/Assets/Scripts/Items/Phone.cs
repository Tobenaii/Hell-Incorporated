﻿using System.Collections;
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
        //Start ringing and store dialogue data for later
        StopPhone();
        m_phoneRingSound.Play();
        m_currentDialogueData = dList;
        m_animator.ResetTrigger("Stop");
        m_animator.SetTrigger("Vibrate");
    }

    public override void OnClick(GameObject hand)
    {
        if (!m_currentDialogueData)
            return;
        //Give dialogue data to dialogue system to open the dialogue
        base.OnClick(hand);
        OpenDialogue(m_currentDialogueData);
        m_currentDialogueData = null;
    }
    public void StopPhone()
    {
        //Stop the ringing and remove dialogue data
        m_phoneRingSound.Stop();
        m_dialogue.CloseDialogue();
        m_animator.SetTrigger("Stop");
        m_animator.ResetTrigger("Vibrate");
        m_currentDialogueData = null;
    }

    public void OpenDialogue(DialogueData data)
    {
        StopPhone();
        m_dialogue.StartDialogue(data);
    }
}
