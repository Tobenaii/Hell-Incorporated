using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganBin : Interactable
{
    [SerializeField]
    private List<GameObjectPool> m_organPools = new List<GameObjectPool>();
    private Pickup m_curOrgan;
    

    public override void OnClick(GameObject hand)
    {
        int index = Random.Range(0, m_organPools.Count);
        Pickup organ = m_organPools[index].GetObject().GetComponent<Pickup>();
        organ.transform.SetParent(null);
        organ.transform.position = hand.transform.position + hand.transform.forward;
        m_curOrgan = organ;
        organ.OnClick(hand);
    }

    public override void OnRelease(GameObject hand)
    {
        m_curOrgan.OnRelease(hand);
    }

    public override void OnHeld(GameObject hand)
    {
        m_curOrgan.OnHeld(hand);
    }
}
