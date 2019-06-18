using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Devil : MonoBehaviour
{
    private enum State { MoveOutOfPortal, LookAround, ShooAway, MoveIntoPortal };

    [SerializeField]
    private GameObjectListSet m_workingImpList;
    [SerializeField]
    private GameObjectValue m_playerProc;
    [SerializeField]
    private AnimState m_moveOutPortalAnim;
    [SerializeField]
    private AnimState m_lookAroundAnim;
    [SerializeField]
    private AnimState m_bobAnim;
    [SerializeField]
    private AnimState m_moveBackIntoPortalAnim;
    [SerializeField]
    private AnimState m_moveToPlayerAnim;
    [SerializeField]
    private AudioSource m_evilLaugh;
    [SerializeField]
    private FloatValue m_soulQuota;
    [SerializeField]
    private float m_soulQuotaIncrease;
    [SerializeField]
    private GameEvent m_devilScaredImpsEvent;
    private AnimState m_currentAnim;
    private bool m_isPissed;
    private float m_waitTimer;
    private State m_state;

    public void Init()
    {
        m_state = State.MoveOutOfPortal;
        m_currentAnim = m_moveOutPortalAnim;
        m_isPissed = (m_workingImpList.Count > 0);
        m_currentAnim.Init(transform);
        m_bobAnim.Init(transform);
    }

    private void OnEnable()
    {
        m_evilLaugh.Play();
    }

    void Update()
    {
        if (m_currentAnim.UpdateAnim(transform))
        {
            switch (m_state)
            {
                case State.MoveOutOfPortal:
                    m_currentAnim = m_lookAroundAnim;
                    m_currentAnim.Init(transform);
                    m_state = State.LookAround;
                    break;
                case State.LookAround:
                    LookForStuffToGetAngryAbout();
                    break;
                case State.ShooAway:
                    for (int i = 0; i < m_workingImpList.Count; i++)
                        m_workingImpList.List[i].GetComponent<Imp>().OnClick(gameObject);
                    LookForStuffToGetAngryAbout();
                    break;
                case State.MoveIntoPortal:
                    gameObject.SetActive(false);
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
            m_state = State.ShooAway;
            m_soulQuota.value += m_soulQuotaIncrease;
            m_devilScaredImpsEvent.Invoke();
        }
        else
        {
            m_currentAnim = m_moveBackIntoPortalAnim;
            m_currentAnim.Init(transform);
            m_state = State.MoveIntoPortal;
        }
    }
}
