using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Processor : MonoBehaviour
{
    [SerializeField]
    private ProcessorListSet m_procListSet;
    [SerializeField]
    private GameObjectPool m_paperPool;
    [SerializeField]
    private Transform m_paperLocation;
    private GameObject m_paperInstance;

    private Soul m_currentSoul;

    private bool m_isProcessing;
    public bool IsProcessing { get { return m_isProcessing; } private set { m_isProcessing = value; } }

    public void StartProcessing(Soul soul)
    {
        m_currentSoul = soul;
        m_paperInstance = m_paperPool.GetObject();
        m_paperInstance.transform.position = m_paperLocation.position;
        m_paperInstance.transform.rotation = m_paperLocation.rotation;
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

    public void SendToHell()
    {
        m_currentSoul.SendToHell();
        IsProcessing = false;
        m_currentSoul = null;
    }
    private void Update()
    {
    }
}
