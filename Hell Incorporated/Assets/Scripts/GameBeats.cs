using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
class BeatData
{
    [SerializeField]
    private float m_timeTillBeat;
    [SerializeField]
    private GameEvent m_event;

    private bool m_invoked = false;

    public void Invoke(float gameTime)
    {
        if (gameTime < m_timeTillBeat)
            return;
        if (m_invoked)
            return;
        m_invoked = true;
        m_event.Invoke();
    }
}

public class GameBeats : MonoBehaviour
{
    [SerializeField]
    private List<BeatData> m_beats;

    private float m_gameTimer;

    public void StartGame()
    {
        m_gameTimer = 8 * 60;
    }
    private void Update()
    {
        m_gameTimer -= Time.deltaTime;

        foreach (BeatData beat in m_beats)
        {
            beat.Invoke(m_gameTimer);
        }
    }
}
