using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Processor : MonoBehaviour
{
    [SerializeField]
    private ProcessorListSet m_procListSet = null;
    [SerializeField]
    private GameObjectPool m_paperPool = null;
    [SerializeField]
    private Transform m_paperLocation = null;
    [SerializeField]
    private FloatEvent m_scoreEvent = null;
    private GameObject m_paperInstance;
    private Soul m_currentSoul;
    [SerializeField]
    private GameObjectValue m_playerProc;
    [SerializeField]
    private bool m_isPlayerProcessor;
    public bool IsPlayerProcessor => m_isPlayerProcessor;
    private float m_aiTimer;
    [SerializeField]
    private float m_minAiSpeed;
    [SerializeField]
    private float m_maxAiSpeed;

    private bool m_isProcessing;
    public bool IsProcessing { get { return m_isProcessing; } private set { m_isProcessing = value; } }

    private bool m_hasPaper;
    public bool HasHaper { get { return m_hasPaper; } private set { m_hasPaper = value; } }

    public void StartProcessing(Soul soul)
    {
        m_currentSoul = soul;
        m_paperInstance = m_paperPool.GetObject();
        m_paperInstance.transform.position = m_paperLocation.position;
        m_paperInstance.transform.rotation = m_paperLocation.rotation;
        HasHaper = true;
    }

    public void Lock()
    {
        IsProcessing = true;
    }

    private void Awake()
    {
        if (m_isPlayerProcessor)
            m_playerProc.value = gameObject;
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
        m_aiTimer = Random.Range(m_minAiSpeed, m_maxAiSpeed);
    }

    public void SendToHell()
    {
        m_currentSoul.SendToHell();
        m_paperInstance.GetComponent<Paper>().Stamp();
        IsProcessing = false;
        m_currentSoul = null;
        HasHaper = false;
        if (m_isPlayerProcessor)
            m_scoreEvent.Invoke(1.0f);
        m_aiTimer = Random.Range(m_minAiSpeed, m_maxAiSpeed);
    }
    private void Update()
    {
        if (m_isPlayerProcessor || !m_hasPaper)
            return;

        m_aiTimer -= Time.deltaTime;
        if (m_aiTimer <= 0)
            SendToHell();
    }
}
