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
    private float m_gameTime;
    [SerializeField]
    private FloatValue m_gameTimer;
    [SerializeField]
    private List<BeatData> m_beats;

    private bool m_gameStarted;

    public void StartGame()
    {
        m_gameTimer.value = 0;
        m_gameStarted = true;
    }
    private void Update()
    {
        if (!m_gameStarted)
            return;

        m_gameTimer.value += Time.deltaTime;
        foreach (BeatData beat in m_beats)
        {
            beat.Invoke(m_gameTimer.value);
        }
    }
}
