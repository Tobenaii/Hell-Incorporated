using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoWorkerAnimation : StateMachineBehaviour
{
    [SerializeField]
    private GameEvent m_scannerAnimCleanup;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_scannerAnimCleanup.Invoke();
    }
}
