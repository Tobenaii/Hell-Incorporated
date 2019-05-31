using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Processor : MonoBehaviour
{
    [SerializeField]
    private ProcessorListSet m_procListSet;

    [SerializeField]
    private float m_processingTime;
    private float m_processTimer;
    private Soul m_currentSoul;

    private bool m_isProcessing;
    public bool IsProcessing { get { return m_isProcessing; } private set { m_isProcessing = value; } }

    public void StartProcessing(Soul soul)
    {
        m_currentSoul = soul;
        m_processTimer = m_processingTime;
        IsProcessing = true;
    }

    public void Lock()
    {
        IsProcessing = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        int index = 0;
        foreach (Processor proc in m_procListSet.List)
        {
            if (proc.gameObject.transform.position.x > transform.position.x)
                index++;
        }
        m_procListSet.Insert(index, this);
    }

    private void Update()
    {
        if (m_currentSoul == null)
            return;
        m_processTimer -= Time.deltaTime;
        if (m_processTimer <= 0)
        {
            m_currentSoul.SendToHell();
            IsProcessing = false;
            m_currentSoul = null;
        }
    }
}
