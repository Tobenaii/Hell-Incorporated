using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField]
    protected ProcState m_procState = null;
    [SerializeField]
    protected GameObject m_arrow;
    [SerializeField]
    protected GameEvent m_tutorialDoneEvent;
    [SerializeField]
    protected GameEvent m_actionEvent;
    protected bool m_inTutorial;

    private void Awake()
    {
        m_arrow.SetActive(false);
    }

    public void StartTutorial()
    {
        m_arrow.SetActive(true);
        m_inTutorial = true;
    }

    public abstract void DoAction();
}
