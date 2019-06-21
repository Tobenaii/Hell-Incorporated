using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganBin : Interactable
{
    [SerializeField]
    private List<GameObjectPool> m_organPools = new List<GameObjectPool>();
    [SerializeField]
    private GameObject m_tutArrow;
    private Pickup m_curOrgan;
    private bool m_tut;

    private void Start()
    {
        m_tutArrow.SetActive(false);
    }

    public void StartTutorial()
    {
        m_tutArrow.SetActive(true);
        m_tut = true;
    }

    public override void OnClick(GameObject hand)
    {
        if (m_tut)
        {
            m_tut = false;
            m_tutArrow.SetActive(false);
        }
        int index = Random.Range(0, m_organPools.Count);
        m_curOrgan?.OnRelease(hand);
        Pickup organ = m_organPools[index].GetObject().GetComponent<Pickup>();
        if (organ == null)
            return;
        organ.transform.SetParent(null);
        organ.transform.position = transform.position;
        m_curOrgan = organ;
        organ.OnClick(hand);
    }

    public override void OnRelease(GameObject hand)
    {
        m_curOrgan?.OnRelease(hand);
    }

    public override void OnHeld(GameObject hand)
    {
        m_curOrgan?.OnHeld(hand);
    }
}
