using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SoulCounter : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro m_text = null;
    private float m_score;

    public void AddToScore(float score)
    {
        m_score += score;
        m_text.text = "Souls: " + m_score.ToString();
    }
}
