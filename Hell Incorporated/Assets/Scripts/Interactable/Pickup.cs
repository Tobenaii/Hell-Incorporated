using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : Interactable
{
    private Transform m_originParent;
    private Rigidbody m_rb;

    private Vector3 m_prevHandPos;

    private bool m_disabled;

    private void Awake()
    {
        m_originParent = transform.parent;
        m_rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        m_disabled = false;

    }

    public override void OnClick(GameObject hand)
    {
        if (m_disabled)
            return;
        base.OnClick(hand);
        transform.SetParent(hand.transform);
        m_rb.freezeRotation = true;
    }

    public override void OnRelease(GameObject hand)
    {
        if (m_disabled)
            return;
        //Apply a force to the object in the direction of movement velocity
        base.OnRelease(hand);
        transform.SetParent(m_originParent);
        m_rb.freezeRotation = false;

        Vector3 forceDir = hand.transform.position - m_prevHandPos;
        float force = 60 * Vector3.Magnitude(forceDir);
        if (force > 200)
            force = 200;
        m_rb.AddForce(Vector3.Normalize(hand.transform.up * -1 + hand.transform.forward) * force, ForceMode.Impulse);
    }

    public void OnDisable()
    {
        m_disabled = true;
        transform.SetParent(m_originParent);
        m_rb.freezeRotation = false;
    }
    public override void OnHeld(GameObject hand)
    {
        if (m_disabled)
            return;
        //Mvoe object towards hand
        base.OnHeld(hand);
        Vector3 direction = hand.transform.position + hand.transform.forward * 0.2f - transform.position;

#if UNITY_EDITOR
        direction = hand.transform.position + hand.transform.forward * 0.5f - transform.position;
#endif
        m_rb.velocity = direction * 15;
        m_prevHandPos = hand.transform.position;
    }
}
