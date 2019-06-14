using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundItem : MonoBehaviour
{
    [SerializeField]
    private Transform m_initialTransform;
    private bool m_dissolve;
    private bool m_resolve;
    private TimeLerper m_timeLerper;
    private float m_dissolveAmmount;
    private bool m_isHeld = false;

    public void Held(bool held)
    {
        m_isHeld = held;
    }

    public void Dissolve()
    {
        m_dissolve = true;
        m_timeLerper = new TimeLerper();
    }

    private void Update()
    {
        if (!m_isHeld && !m_dissolve)
        {
            if (Vector3.Distance(transform.position, m_initialTransform.position) > 0.05f)
            {
                Dissolve();
            }
        }
        if (m_dissolve)
        {
            if (m_resolve)
            {
                m_dissolveAmmount = m_timeLerper.Lerp(1, 0, 0.5f);
                GetComponent<Renderer>().material.SetFloat("_Amount", m_dissolveAmmount);
                if (m_dissolveAmmount == 0)
                {
                    m_dissolve = false;
                    m_resolve = false;
                    m_timeLerper.Reset();
                }
                return;
            }
            m_dissolveAmmount = m_timeLerper.Lerp(0, 1, 0.5f);
            GetComponent<Renderer>().material.SetFloat("_Amount", m_dissolveAmmount);
            if (m_dissolveAmmount == 1)
            {
                m_resolve = true;
                m_timeLerper.Reset();
                transform.position = m_initialTransform.position;
                transform.rotation = m_initialTransform.rotation;
                GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
            return;
        }
    }
}
