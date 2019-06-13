using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField]
    private GameEvent m_tutorialDialogueEvent;
    [SerializeField]
    private GameEvent m_beat1Event;

    private void Start()
    {
        m_beat1Event.Invoke();
    }

    public void StartNextTutorial()
    {
        m_tutorialDialogueEvent.Invoke();
    }
}
