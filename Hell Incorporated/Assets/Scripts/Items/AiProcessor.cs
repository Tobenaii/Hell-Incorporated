using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AiProcessor : Processor
{
    private float m_aiTimer;
    [SerializeField]
    private float m_minAiSpeed;
    [SerializeField]
    private float m_maxAiSpeed;
    [SerializeField]
    private float m_distractionTime;
    [SerializeField]
    private GameObject m_distractedSpeech;
    private float m_distractedTimer;
    private bool m_isDistracted;

    private void Awake()
    {
        m_aiTimer = Random.Range(m_minAiSpeed, m_maxAiSpeed);
        m_distractedTimer = m_distractionTime;
        m_distractedSpeech.SetActive(false);
    }

    private void Update()
    {
        if (!m_hasPaper)
            return;

        //If the AI is distracted, stop them from processing and bring up the distracted dialogue box
        if (m_isDistracted)
        {
            m_distractedTimer -= Time.deltaTime;
            m_distractedSpeech.SetActive(true);
            if (m_distractedTimer <= 0)
            {
                //Enable processing again
                m_isDistracted = false;
                m_distractedSpeech.SetActive(false);
            }
            return;
        }

        m_aiTimer -= Time.deltaTime;
        //Send the souls to hell when the timer hits 0
        if (m_aiTimer <= 0)
            SendToHell();
    }

    public override void SendToHell()
    {
        //Sens soul to hell and reset timer to random int between range
        base.SendToHell();
        m_aiTimer = Random.Range(m_minAiSpeed, m_maxAiSpeed);
    }

    public void Distract()
    {
        //Disable processing
        m_isDistracted = true;
        m_distractedTimer = m_distractionTime;
    }
}
