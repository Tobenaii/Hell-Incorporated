using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField]
    private GameEvent m_tutorialDialogueEvent;

    private void Start()
    {
        m_tutorialDialogueEvent.Invoke();
    }

    public void StartNextTutorial()
    {
        m_tutorialDialogueEvent.Invoke();
    }
}
