using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Animation/LookAround")]
public class LookAroundAnim : AnimState
{
    private enum RotationState { RotateLeft, RotateRight };

    [SerializeField]
    private float m_maxLookAngle;
    [SerializeField]
    private float m_rotationSpeed;
    Quaternion m_rotation1;
    Quaternion m_rotation2;
    RotationState m_state;

    public override void Init(Transform transform)
    {
        Quaternion initRot = transform.rotation;
        transform.Rotate(transform.up, -m_maxLookAngle);
        m_rotation1 = transform.rotation;
        transform.rotation = initRot;
        transform.Rotate(transform.up, m_maxLookAngle);
        m_rotation2 = transform.rotation;
        transform.rotation = initRot;
        m_state = RotationState.RotateLeft;
    }

    public override bool UpdateAnim(Transform transform)
    {
        if (m_state == RotationState.RotateLeft)
        {
            if (transform.rotation != m_rotation1)
                transform.rotation = Quaternion.RotateTowards(transform.rotation, m_rotation1, m_rotationSpeed * Time.deltaTime);
            else
                m_state = RotationState.RotateRight;
            return false;

        }
        else
        {
            if (transform.rotation != m_rotation2)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, m_rotation2, m_rotationSpeed * Time.deltaTime);
                return false;
            }
            else
                m_state = RotationState.RotateLeft;
        }
        return true;
    }
}
