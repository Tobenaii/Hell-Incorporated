﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogue : Interactable
{
    [SerializeField]
    private float m_dialogueSpeed = 0;
    [SerializeField]
    private TextMeshPro m_text = null;
    [SerializeField]
    protected GameObject m_dialogueBox;

    private bool m_isDialoging;
    private bool m_waitingForPage;
    private List<string> m_cutDialogue = new List<string>();
    private int m_currentPage;
    private int m_currentLetter;
    private float m_timer;
    private DialogueData m_currentDialogue;

    public void CloseDialogue()
    {
        m_text.text = "";
        m_dialogueBox.SetActive(false);
        m_isDialoging = false;
        GetComponent<BoxCollider>().enabled = false;
    }

    public void StartDialogue(DialogueData data)
    {
        m_dialogueBox.SetActive(true);
        m_currentDialogue = data;
        StartDialogue();
    }

    private void StartDialogue()
    {
        GetComponent<BoxCollider>().enabled = true;
        m_text.text = "";
        m_cutDialogue.Clear();
        string page = "";
        foreach (string word in m_currentDialogue.Dialogue.Split(' '))
        {
            if (word == "/page")
            {
                m_cutDialogue.Add(page);
                m_text.text = "";
                page = "";
                continue;
            }
            m_text.text += word + " ";
            m_text.ForceMeshUpdate();
            if (!m_text.isTextOverflowing)
                page += word + " ";
            else
            {
                m_cutDialogue.Add(page);
                m_text.text = "";
                page = word + " ";
            }
        }
        m_cutDialogue.Add(page);
        m_text.text = "";
        m_isDialoging = true;
        m_currentPage = 0;
        m_currentLetter = 0;
        m_timer = m_dialogueSpeed;
        m_waitingForPage = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_isDialoging)
            return;

        if (m_waitingForPage)
            return;

        m_timer -= Time.deltaTime;
        if (m_timer <= 0)
        {
            if (m_currentLetter == m_cutDialogue[m_currentPage].Length)
            {
                m_currentLetter = 0;
                m_waitingForPage = true;
                return;
            }
            m_timer = m_dialogueSpeed;
            m_text.text += m_cutDialogue[m_currentPage][m_currentLetter];
            m_currentLetter++;

        }
    }

    public override void OnClick(GameObject hand)
    {
        base.OnClick(hand);
        if (!m_isDialoging)
            return;
        if (m_waitingForPage)
        {
            m_waitingForPage = false;
            m_currentPage++;
            if (m_currentDialogue.CloseOnLastPage || m_currentPage < m_cutDialogue.Count)
                m_text.text = "";
            m_currentLetter = 0;
            if (m_currentPage == m_cutDialogue.Count)
            {
                GetComponent<BoxCollider>().enabled = false;
                if (m_currentDialogue.CloseOnLastPage)
                {
                    m_dialogueBox.SetActive(false);
                }
                m_isDialoging = false;
            }
        }
        else
        {
            m_text.text = m_cutDialogue[m_currentPage];
            m_currentLetter = m_cutDialogue[m_currentPage].Length;
            m_waitingForPage = true;
        }
    }
}
