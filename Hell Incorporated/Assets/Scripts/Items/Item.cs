using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    protected ProcState m_procState = null;
    [SerializeField]
    protected GameObject m_arrow;
    protected bool m_tut = false;

    private void Awake()
    {
        m_arrow.SetActive(false);
    }

    public void StartTutorialArrow()
    {
        m_tut = true;
    }
}
