using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SoulCounter : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro m_text = null;
    [SerializeField]
    private FloatValue m_quota;

    public void UpdateQuota()
    {
        m_text.text = "Souls remaining: " + m_quota.value.ToString();
    }
}
