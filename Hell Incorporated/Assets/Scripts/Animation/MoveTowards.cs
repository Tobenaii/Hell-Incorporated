using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Animation/MoveTowards")]
public class MoveTowards : AnimState
{
    [SerializeField]
    private GameObjectValue m_target;
    [SerializeField]
    private float m_moveTime;
    [SerializeField]
    private bool m_stareAtTarget;
    [SerializeField]
    private float m_forwardOffset;
    [SerializeField]
    private float m_rightOffset;
    [SerializeField]
    private float m_upOffset;
    [SerializeField]
    private bool m_lockX;
    [SerializeField]
    private bool m_lockY;
    [SerializeField]
    private bool m_lockZ;
    [SerializeField]
    private bool m_checkX = true;
    [SerializeField]
    private bool m_checkY = true;
    [SerializeField]
    private bool m_checkZ = true;
    Vector3 m_initPos;
    TimeLerper m_lerper = new TimeLerper();

    public override void Init(Transform transform)
    {
        m_initPos = transform.position;
        m_lerper.Reset();
    }

    public override bool UpdateAnim(Transform transform)
    {
        //Get new target from target and offset
        Vector3 offset = m_target.value.transform.forward * m_forwardOffset + m_target.value.transform.right * m_rightOffset + m_target.value.transform.up * m_upOffset;
        Vector3 target = m_target.value.transform.position + offset;
        //Lock axis so it doesn't move along it but checks still work
        if (m_lockX)
            target.x = transform.position.x;
        if (m_lockY)
            target.y = transform.position.y;
        if (m_lockZ)
            target.z = transform.position.z;
        //Only check along these axes
        Vector3 check = transform.position;
        if (m_checkX)
            check.x = target.x;
        if (m_checkY)
            check.y = target.y;
        if (m_checkZ)
            check.z = target.z;
        //Move and check for target pos
        if (Vector3.Distance(transform.position, check) > 0.1f)
        {
            transform.position = m_lerper.Lerp(m_initPos, target, m_moveTime);
            if (m_stareAtTarget)
                transform.LookAt(new Vector3(m_target.value.transform.position.x, transform.position.y, m_target.value.transform.position.z));
            return false;
        }
        return true;
    }
}
