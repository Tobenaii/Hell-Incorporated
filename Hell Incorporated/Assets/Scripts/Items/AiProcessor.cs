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

        if (m_isDistracted)
        {
            m_distractedTimer -= Time.deltaTime;
            m_distractedSpeech.SetActive(true);
            if (m_distractedTimer <= 0)
            {
                m_isDistracted = false;
                m_distractedSpeech.SetActive(false);
            }
            return;
        }

        m_aiTimer -= Time.deltaTime;
        if (m_aiTimer <= 0)
            SendToHell();
    }

    public override void SendToHell()
    {
        base.SendToHell();
        m_aiTimer = Random.Range(m_minAiSpeed, m_maxAiSpeed);
    }

    public void Distract()
    {
        m_isDistracted = true;
        m_distractedTimer = m_distractionTime;
    }
}
