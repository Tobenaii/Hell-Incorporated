﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Devil : MonoBehaviour
{
    [SerializeField]
    private GameObjectListSet m_workingImpList;
    [SerializeField]
    private GameObjectValue m_playerProc;
    [SerializeField]
    private AudioSource m_evilLaugh;
    [SerializeField]
    private FloatValue m_soulQuota;
    [SerializeField]
    private float m_soulQuotaIncrease;
    [SerializeField]
    private GameEvent m_devilScaredImpsEvent;
    [SerializeField]
    private Animator m_animator;
    private bool m_isPissed;
    private float m_waitTimer;

    public void Init()
    {
        m_evilLaugh.Play();
        m_isPissed = (m_workingImpList.Count > 0);
        m_animator.SetTrigger("MoveOut");
    }

    private void OnEnable()
    {
    }

    void Update()
    {
    }

    public void OnLookAround()
    {
        LookForStuffToGetAngryAbout();
    }

    private void LookForStuffToGetAngryAbout()
    {
        //Check if there are any working imps in the list
        foreach (GameObject imp in m_workingImpList.List)
        {
            if (imp != null)
            {
                //If there are working imps, add to soul quota and trigger animation
                m_soulQuota.value += m_soulQuotaIncrease;
                m_animator.SetTrigger("GetAngry");
                return;
            }
        }
    }

    public void ScareImps()
    {
        //Scare all the imps away (this is called on the frame that the devil scares away imps in its animation)
        for (int i = 0; i < 3; i++)
        {
            if (m_workingImpList.List[i])
            {
                m_workingImpList.List[i].GetComponent<Imp>().FlyAway();
            }
        }
    }
}
