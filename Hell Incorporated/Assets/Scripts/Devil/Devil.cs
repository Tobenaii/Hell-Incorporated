using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Devil : MonoBehaviour
{
    [SerializeField]
    private GameObjectListSet m_workingImpList;
    [SerializeField]
    private ProcessorListSet m_procListSet;
    private GameObject m_playerProc;
    private bool m_isPissed;
    private float m_waitTimer;


    private void OnEnable()
    {
        m_isPissed = (m_workingImpList.Count > 0);

        if (m_isPissed)
        {
            foreach (Processor proc in m_procListSet.List)
            {
                if (proc.IsPlayerProcessor)
                {
                    m_playerProc = proc.gameObject;
                    break;
                }
            }
        }
        else
            m_waitTimer = 5;
    }

    void Update()
    {
        if (!m_isPissed)
        {
            m_waitTimer -= Time.deltaTime;
            if (m_waitTimer <= 0)
                gameObject.SetActive(false);
        }
        if (m_playerProc == null || !m_isPissed)
            return;

        transform.LookAt(new Vector3(m_playerProc.transform.position.x, transform.position.y, m_playerProc.transform.position.z));
        transform.position = Vector3.MoveTowards(transform.position, m_playerProc.transform.position, 10 * Time.deltaTime);
    }
}
