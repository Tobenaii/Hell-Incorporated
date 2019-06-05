using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Devil : MonoBehaviour
{
    [SerializeField]
    private GameObjectListSet m_workingImpList;
    [SerializeField]
    private GameObjectValue m_playerProc;
    private bool m_isPissed;
    private float m_waitTimer;


    private void OnEnable()
    {
        m_isPissed = (m_workingImpList.Count > 0);
        Debug.Log(m_playerProc.value.name);
    }

    void Update()
    {

    }
}
