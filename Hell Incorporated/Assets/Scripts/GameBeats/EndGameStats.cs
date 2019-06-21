using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGameStats : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro m_text;
    [SerializeField]
    private DigitalClock m_clock;
    [SerializeField]
    private FloatValue m_soulQuota;
    [SerializeField]
    private GameEvent m_wonGameEvent;
    [SerializeField]
    private GameEvent m_lostGameEvent;


    public void ShowStats()
    {
        if (m_soulQuota.value <= 0)
        {
            m_text.text = "Congratulatory text congratulating you for your achievenemts. Congratulations! You finished your shift at " + m_clock.GetDigitalTime();
            m_wonGameEvent.Invoke();
        }
        else
        {
            m_text.text = "Unfortunately you still had " + m_soulQuota.value.ToString() + " souls remaining in your daily quota.";
            m_lostGameEvent.Invoke();
        }
    }
}
