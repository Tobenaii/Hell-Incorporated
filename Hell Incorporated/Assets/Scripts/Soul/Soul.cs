using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour
{
    private Transform m_hellDoor = null;
    [SerializeField]
    private float m_moveSpeed = 0;
    [SerializeField]
    private float m_goingToHellMoveSpeed = 0;
    [SerializeField]
    private GameObjectPool m_soulPool = null;

    private Processor m_currentProcessor;

    private bool m_sendingToHell;

    // Start is called before the first frame update
    void Start()
    {
        m_hellDoor = GameObject.Find("PortalEffect").transform;   
    }

    public bool FindProcessor(ProcessorListSet procListSet)
    {
        foreach (Processor proc in procListSet.List)
        {
            if (!proc.IsProcessing)
            {
                m_currentProcessor = proc;
                proc.Lock();
                return true;
            }
        }
        return false;
    }

    public void SendToHell()
    {
        m_sendingToHell = true;
    }

    public void Update()
    {
        if (m_sendingToHell)
        {
            transform.position = Vector3.MoveTowards(transform.position, m_hellDoor.position, m_goingToHellMoveSpeed * Time.deltaTime);
            if (transform.position == m_hellDoor.position)
            {
                m_sendingToHell = false;
                m_soulPool.DestroyObject(gameObject);
            }
        }
        if (m_currentProcessor == null)
            return;
        if (transform.position.x != m_currentProcessor.gameObject.transform.position.x)
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(m_currentProcessor.transform.position.x, transform.position.y, transform.position.z), m_moveSpeed * Time.deltaTime);
        else if (transform.position.z != m_currentProcessor.transform.position.z)
            transform.position = Vector3.MoveTowards(transform.position, m_currentProcessor.transform.position, m_moveSpeed * Time.deltaTime);
        else
        {
            m_currentProcessor.StartProcessing(this);
            m_currentProcessor = null;
        }
    }
}
