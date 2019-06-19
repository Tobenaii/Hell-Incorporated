using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField]
    private GameEvent m_failedGameEvent;
    [SerializeField]
    private GameEvent m_wonGameEvent;
    [SerializeField]
    private GameEvent m_endGameEvent;
    [SerializeField]
    private FloatValue m_soulQuota;
    [SerializeField]
    private GameObjectListSet m_workingImpList;
    private bool m_gameEnded;

    private void Update()
    {
        if (m_gameEnded)
            return;
        if (m_soulQuota.value <= 0)
        {
            m_gameEnded = true;
            m_soulQuota.value = 0;
            m_wonGameEvent.Invoke();
            m_endGameEvent.Invoke();
            for (int i = 0; i < 3; i++)
                m_workingImpList.List[i].GetComponent<Imp>().FlyAway();
        }
    }

    public void Restart()
    {
        m_gameEnded = false;
    }

    public void FinishGame()
    {
        if (m_soulQuota.value <= 0)
            m_wonGameEvent.Invoke();
        else
            m_failedGameEvent.Invoke();
    }
}
