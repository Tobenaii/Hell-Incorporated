using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Worker : MonoBehaviour
{
    [SerializeField]
    private GameObjectListSet m_impWorkers = null;
    [SerializeField]
    private int m_workerNumber = 0;
    [SerializeField]
    protected float m_workSpeed = 0;
    [SerializeField]
    private ProcState m_procState = null;
    [SerializeField]
    private protected Processor m_processor = null;
    protected GameObject m_imp;
    protected bool m_startedState;
    bool m_isWorking;
    bool m_init;

    private void Update()
    {
        if (m_imp == null)
        {
            Init();
            return;
        }
        if (!m_imp.GetComponent<Imp>().IsWorking)
        {
            Init();
        }

        if (MoveToPosition(m_imp.transform, transform.position, 5.0f))
        {
            if (MoveToRotation(m_imp.transform, transform.rotation, 100.0f))
            {
                if (!m_init)
                {
                    InitWorker();
                    m_init = true;
                }
            }
        }

        if (m_init)
        {
            if (!m_startedState && (int)m_procState.state != m_workerNumber)
                return;
            DoAction();
        }
        //if (m_impWorkers.List.Count > m_workerNumber)
        //{
        //    if (!m_isWorking)
        //    {
        //        m_isWorking = true;
        //        Init();
        //    }
        //    if (MoveToPosition(m_imp.transform, transform.position, 5.0f))
        //    {
        //        if (MoveToRotation(m_imp.transform, transform.rotation, 100.0f))
        //        {
        //            if (!m_init)
        //            {
        //                InitWorker();
        //                m_init = true;
        //            }
        //        }
        //    }
        //    if (m_init)
        //    {
        //        if (!m_startedState && (int)m_procState.state != m_workerNumber)
        //            return;
        //        DoAction();
        //    }
        //}
    }
    protected abstract void DoAction();
    protected abstract void InitWorker();

    private void Init()
    {
        m_imp = null;
        m_init = false;
        foreach (GameObject imp in m_impWorkers.List)
        {
            Imp i = imp.GetComponent<Imp>();
            if (!i.IsWorking)
            {
                i.SetWorking();
                m_imp = imp;
                m_imp.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }

    protected bool MoveToPosition(Transform t, Vector3 v, float speed)
    {
        t.position = Vector3.MoveTowards(t.position, v, speed * Time.deltaTime);
        return (Vector3.Distance(t.position, v) < 0.001f);
    }

    protected bool MoveToRotation(Transform t, Quaternion q, float speed)
    {
        t.rotation = Quaternion.RotateTowards(t.rotation, q, speed * Time.deltaTime);
        return (t.rotation == q);
    }
}
