using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    //This class is used to invoke events when it is interacted with (called by HandPointer)
    [SerializeField]
    private GameEvent m_onClick;
    [SerializeField]
    private GameEvent m_onRelease;
    [SerializeField]
    private UnityEvent m_onHoverEnter;


    public virtual void OnClick(GameObject hand)
    {
        if (m_onClick == null)
            return;
        m_onClick.Invoke();
    }

    public virtual void OnRelease(GameObject hand)
    {
        if (m_onRelease == null)
            return;
        m_onRelease.Invoke();
    }

    public virtual void OnHeld(GameObject hand)
    {
    }

    public virtual void OnHoverEnter(GameObject hand)
    {
        m_onHoverEnter.Invoke();
    }
}
