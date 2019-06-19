using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DigitalClock : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro m_text;
    [SerializeField]
    private FloatValue m_gameTime;
    private int m_hour;
    private int m_minute;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public string GetDigitalTime()
    {
        return ((m_hour <= 12) ? m_hour.ToString() : (m_hour - 12).ToString()) + ":" + ((m_minute < 10) ? "0" : "") + m_minute.ToString() + ((m_hour >= 12) ? " PM" : " AM");
    }

    // Update is called once per frame
    void Update()
    {
        m_hour = ((int)m_gameTime.value / 60) + 9;
        m_minute = (int)Mathf.Repeat(m_gameTime.value, 60);
        m_text.text = GetDigitalTime();
    }
}
