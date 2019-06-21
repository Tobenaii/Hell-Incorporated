using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHeart : MonoBehaviour
{
    [SerializeField]
    private Animator m_animator;

    private void Start()
    {
        m_animator.enabled = false;
    }

    public void StartBeating()
    {
        m_animator.enabled = true;
    }
}
