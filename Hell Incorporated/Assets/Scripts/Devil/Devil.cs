using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Devil : MonoBehaviour
{
    private enum State { LookAround, WalkAround };

    [SerializeField]
    private GameObjectListSet m_workingImpList;
    [SerializeField]
    private GameObjectValue m_playerProc;
    [SerializeField]
    private AnimState m_lookAroundAnim;
    [SerializeField]
    private AnimState m_bobAnim;
    [SerializeField]
    private AnimState m_moveToPlayerAnim;
    private AnimState m_currentAnim;
    private bool m_isPissed;
    private float m_waitTimer;
    private State m_state;

    private void OnEnable()
    {
        m_state = State.LookAround;
        m_currentAnim = m_lookAroundAnim;
        m_isPissed = (m_workingImpList.Count > 0);
        m_currentAnim.Init(transform);
        m_bobAnim.Init(transform);
    }

    void Update()
    {
        if (m_currentAnim.UpdateAnim(transform))
        {
            switch (m_state)
            {
                case State.LookAround:
                    LookForStuffToGetAngryAbout();
                    break;
            }
        }
        m_bobAnim.UpdateAnim(transform);
    }

    private void LookForStuffToGetAngryAbout()
    {
        if (m_workingImpList.Count > 0)
        {
            m_currentAnim = m_moveToPlayerAnim;
            m_currentAnim.Init(transform);
        }
        else
            gameObject.SetActive(false);
    }
}
