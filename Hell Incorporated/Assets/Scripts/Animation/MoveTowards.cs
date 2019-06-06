using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Animation/MoveTowards")]
public class MoveTowards : AnimState
{
    [SerializeField]
    private GameObjectValue m_target;
    [SerializeField]
    private float m_moveSpeed;
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

    public override void Init(Transform transform)
    {
        
    }

    public override bool UpdateAnim(Transform transform)
    {
        Vector3 offset = m_target.value.transform.forward * m_forwardOffset + m_target.value.transform.right * m_rightOffset + m_target.value.transform.up * m_upOffset;
        Vector3 target = m_target.value.transform.position + offset;
        if (m_lockX)
            target.x = transform.position.x;
        if (m_lockY)
            target.y = transform.position.y;
        if (m_lockZ)
            target.z = transform.position.z;
        if (Vector3.Distance(transform.position, target) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, m_moveSpeed * Time.deltaTime);
            if (m_stareAtTarget)
                transform.LookAt(new Vector3(m_target.value.transform.position.x, transform.position.y, m_target.value.transform.position.z));
            return false;
        }
        return true;
    }
}
