﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Processor : MonoBehaviour
{
    [SerializeField]
    protected ProcessorListSet m_procListSet = null;
    [SerializeField]
    private GameObjectPool m_paperPool = null;
    [SerializeField]
    private Transform m_paperLocation = null;

    private GameObject m_paperInstance;
    private Soul m_currentSoul;

    private bool m_isProcessing;
    public bool IsProcessing { get { return m_isProcessing; } private set { m_isProcessing = value; } }

    protected bool m_hasPaper;
    public bool HasPaper { get { return m_hasPaper; } private set { m_hasPaper = value; } }

    public virtual void StartProcessing(Soul soul)
    {
        m_currentSoul = soul;
        m_paperInstance = m_paperPool.GetObject();
        m_paperInstance.transform.position = m_paperLocation.position;
        m_paperInstance.transform.rotation = m_paperLocation.rotation;

        HasPaper = true;
    }

    public void Lock()
    {
        IsProcessing = true;
    }

    // Start is called before the first frame update
    protected void Start()
    {
        Init();
    }

    protected void Init()
    {
        //Add this processor to the list based on position relative to other processors already in list
        int index = 0;
        foreach (Processor proc in m_procListSet.List)
        {
            if (proc.gameObject.transform.position.x > transform.position.x)
                index++;
        }
        m_procListSet.Insert(index, this);
    }

    public virtual void SendToHell()
    {
        if (!m_currentSoul)
            return;
        //Stamp the paper and send it to hell
        m_currentSoul.SendToHell();
        m_paperInstance.GetComponent<Paper>().Stamp();
        IsProcessing = false;
        m_currentSoul = null;
        HasPaper = false;
    }
    private void Update()
    {


    }
}
