using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField]
    private AnimState m_bobAnim;

    private void Start()
    {
        m_bobAnim.Init(transform);
    }

    private void Update()
    {
        m_bobAnim.UpdateAnim(transform);
    }
}
