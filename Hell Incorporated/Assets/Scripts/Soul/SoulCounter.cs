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

    private void Update()
    {
        m_text.text = m_quota.value.ToString();
    }
}
