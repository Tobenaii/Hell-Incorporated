using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField]
    private AnimState m_bobAnim;
    [SerializeField]
    private AudioSource m_audio;

    private void Start()
    {
        m_bobAnim.Init(transform);
        m_audio?.Play();
    }

    private void Update()
    {
        m_bobAnim.UpdateAnim(transform);
    }
}
