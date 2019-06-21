using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour
{
    [SerializeField]
    private GameObjectValue m_hellDoor = null;
    [SerializeField]
    private float m_moveSpeed = 0;
    [SerializeField]
    private float m_goingToHellMoveSpeed = 0;
    [SerializeField]
    private GameObjectPool m_soulPool = null;

    private Processor m_currentProcessor;

    private bool m_sendingToHell;

    // Start is called before the first frame update
    public bool FindProcessor(ProcessorListSet procListSet)
    {
        //Find a processor that isn't currently processing
        foreach (Processor proc in procListSet.List)
        {
            if (!proc.IsProcessing)
            {
                //If we've found one, lock it so that no other soul can assign itself to it
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
        //Lots of scripted animation so that the soul moves to the different locations on the way to the processor that it's assigned to
        if (m_sendingToHell)
        {
            transform.position = Vector3.MoveTowards(transform.position, m_hellDoor.value.transform.position, m_goingToHellMoveSpeed * Time.deltaTime);
            if (transform.position == m_hellDoor.value.transform.position)
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
