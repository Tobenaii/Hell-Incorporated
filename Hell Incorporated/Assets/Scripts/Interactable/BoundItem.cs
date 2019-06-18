using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundItem : Interactable
{
    private TimeLerper m_timeLerper;
    private float m_dissolveAmmount;
    private List<Material> m_materials = new List<Material>();
    private bool m_isHeld;
    private bool m_dissolving;

    private Vector3 m_initialPos;
    private Quaternion m_initialRot;

    private void Start()
    {

        foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
        {
            foreach (Material mat in renderer.materials)
                m_materials.Add(mat);
        }
        m_initialPos = transform.position;
        m_initialRot = transform.rotation;
        m_timeLerper = new TimeLerper();
        m_dissolveAmmount = 0;
    }

    public override void OnClick(GameObject hand)
    {
        base.OnClick(hand);
        m_isHeld = true;
    }

    public override void OnHeld(GameObject hand)
    {
        base.OnHeld(hand);
    }

    public override void OnRelease(GameObject hand)
    {
        base.OnRelease(hand);
        m_isHeld = false;
    }

    private IEnumerator Dissolve()
    {
        //Dissolve object then move it back to it's initial position
        m_dissolving = true;
        m_timeLerper.Reset();
        while (m_dissolveAmmount != 1)
        {
            m_dissolveAmmount = m_timeLerper.Lerp(0, 1, 0.5f);
            foreach (Material mat in m_materials)
                mat.SetFloat("_Amount", m_dissolveAmmount);
            yield return null;
        }
        transform.position = m_initialPos;
        transform.rotation = m_initialRot;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        StartCoroutine(Resolve());
    }

    private IEnumerator Resolve()
    {
        //Resolve object
        m_timeLerper.Reset();
        while (m_dissolveAmmount != 0)
        {
            m_dissolveAmmount = m_timeLerper.Lerp(1, 0, 0.5f);
            foreach (Material mat in m_materials)
                mat.SetFloat("_Amount", m_dissolveAmmount);
            yield return null;
        }
        m_dissolving = false;
    }

    private void Update()
    {
        //If the object isn't being held, and it's not at its initial position, dissolve it
        if (!m_isHeld && !m_dissolving)
        {
            if (Vector3.Distance(transform.position, m_initialPos) > 0.05f)
                StartCoroutine(Dissolve());
        }
    }
}
