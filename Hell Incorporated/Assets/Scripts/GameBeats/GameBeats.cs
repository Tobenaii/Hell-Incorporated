using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
class BeatData
{
    //Class for holding game beat information. After the gameTime has passed timeTillBeat, invoke the event
    [SerializeField]
    private float m_timeTillBeat;
    [SerializeField]
    private GameEvent m_event;

    private bool m_invoked = false;

    public void Reset()
    {
        m_invoked = false;
    }

    public void Invoke(float gameTime)
    {
        //Check if gameTIme has passed the beat time
        if (gameTime < m_timeTillBeat)
            return;
        if (m_invoked)
            return;
        //Invoke event only once
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

    public void RestartGame()
    {
        //Resets all beats so they can be invoked again
        m_gameTimer.value = 0;
        m_gameStarted = true;
        foreach (BeatData beat in m_beats)
            beat.Reset();
    }

    public void StopGame()
    {
        m_gameStarted = false;
    }

    public void StartGame()
    {
        m_gameTimer.value = 0;
        m_gameStarted = true;
    }
    private void Update()
    {
        if (!m_gameStarted)
            return;
        //Update the beats
        m_gameTimer.value += Time.deltaTime;
        foreach (BeatData beat in m_beats)
        {
            beat.Invoke(m_gameTimer.value);
        }
    }
}
