using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoWorker : MonoBehaviour
{
    [SerializeField]
    protected float m_workSpeed = 0;
    [SerializeField]
    private int m_workerIndex;
    [SerializeField]
    protected ProcState m_procState = null;
    [SerializeField]
    protected GameObjectListSet m_workingImpList;
    [SerializeField]
    protected BoundItem m_boundItem;
    [SerializeField]
    protected Item m_item;
    [SerializeField]
    protected Animator m_animator;

    private bool m_hasImp;
    private bool m_doingAction;
    private Imp m_impObj;

    private void Awake()
    {
        m_animator.enabled = false;
    }

    private void Update()
    {
        if (m_hasImp)
        {
            if (m_workingImpList.List[m_workerIndex] == null)
            {
                //Give back control to the player if there is no imp anymore
                m_hasImp = false;
                GiveBackControl();
                return;
            }
            //Move the imp towards the imp location in the cubicle
                if (!m_doingAction)
                StartCoroutine(GetImp());
            return;
        }
        CheckForImp();
    }

    private void CheckForImp()
    {
        //Check if there is an imp in the workingImpList at the location of this worker index
        if (m_workingImpList.Count < m_workerIndex + 1)
            m_workingImpList.Add(null);
        else if (m_workingImpList.List[m_workerIndex])
        {
            m_hasImp = true;
            m_impObj = m_workingImpList.List[m_workerIndex].GetComponent<Imp>();
        }
    }

    private IEnumerator GetImp()
    {
        //Move the imp towards the assigned imp location in the cubicle
        GameObject imp = m_workingImpList.List[m_workerIndex];
        imp.transform.rotation = transform.rotation;
        while (Vector3.Distance(imp.transform.position, transform.position) > 0.005f)
        {
            imp.transform.position = Vector3.MoveTowards(imp.transform.position, transform.position, 1 * Time.deltaTime);
            yield return null;
        }
        DoAction();
    }

    private void DoAction()
    {
        if (m_doingAction)
            return;
        //If the current state is the same as the worker index (scan = 0, type = 1, stamp = 2), run the animation 
        if (m_procState.state == (ProcState.ProcessorState)m_workerIndex)
        {
            m_boundItem.enabled = false;
            m_boundItem.GetComponent<Pickup>().enabled = false;
            m_boundItem.GetComponent<Rigidbody>().isKinematic = true;
            m_animator.enabled = true;
            m_animator.SetTrigger("DoAction");
            m_doingAction = true;
        }
    }

    public void TriggerAction()
    {
        //Trigger the item action when the animation is complete
        m_item.DoAction();
        m_doingAction = false;
    }

    private void GiveBackControl()
    {
        //Let the player interact with the item again
        m_boundItem.enabled = true;
        m_boundItem.GetComponent<Pickup>().enabled = true;
        m_boundItem.GetComponent<Rigidbody>().isKinematic = false;
        m_animator.enabled = false;
        m_doingAction = false;
    }
}
