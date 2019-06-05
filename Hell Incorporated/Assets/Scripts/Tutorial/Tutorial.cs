using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField]
    private GameEvent m_tutorialEvent;

    private void Start()
    {
        m_tutorialEvent.Invoke();
    }
}
