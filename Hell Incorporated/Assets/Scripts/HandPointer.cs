using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static OVRInput;

public class HandPointer : MonoBehaviour
{
    private LineRenderer m_line;
    private GameObject m_heldObject;
    private List<Interactable> m_interacts = new List<Interactable>();
    private List<Interactable> m_hoverInteracts = new List<Interactable>();
    private GameObject m_previousHoverObject;

    // Start is called before the first frame update
    void Start()
    {
        m_line = GetComponent<LineRenderer>();
    }

    private void OnClick(GameObject obj)
    {
        if (m_interacts.Count == 0)
            return;
        //Call OnClick on the interactable scripts
        m_heldObject = obj;
        foreach (Interactable interact in m_interacts)
            interact.OnClick(gameObject);        
        m_line.enabled = false;
    }

    public void OnRelease()
    {
        //Call OnRelease on the interactable scripts
        foreach (Interactable interact in m_interacts)
            interact.OnRelease(gameObject);
        m_interacts.Clear();
        m_heldObject = null;
        m_line.enabled = true;
    }

    private void OnHoverEnter(GameObject obj)
    {
        foreach (Interactable interact in m_hoverInteracts)
            interact.OnHoverEnter(gameObject);
    }

    void Update()
    {
        //Create a ray from the pointer position towards it's forward vector
        Ray ray = new Ray(transform.position - transform.forward * 0.1f, transform.forward);

#if UNITY_EDITOR
        //If we're in the unity editor we need to account for the mouse position
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        transform.LookAt((ray.origin - transform.forward * 0.1f) + ray.direction * 500);
#endif

        m_line.SetPosition(0, ray.origin);
        //Check for raycast hit on Interactable objects
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000, 1 << 9))
        {
            if (m_previousHoverObject != hit.transform.gameObject)
            {
                m_hoverInteracts = new List<Interactable>(hit.transform.GetComponents<Interactable>());
                OnHoverEnter(hit.transform.gameObject);
                m_hoverInteracts.Clear();
            }
            m_previousHoverObject = hit.transform.gameObject;
            if (OVRInput.GetDown(Button.PrimaryIndexTrigger) || Input.GetMouseButtonDown(0))
            {
                foreach (Interactable i in hit.transform.GetComponents<Interactable>())
                    m_interacts.Add(i);
                OnClick(hit.transform.gameObject);
            }
            m_line.SetPosition(1, hit.point);
        }
        else
        {
            m_previousHoverObject = null;
            m_line.SetPosition(1, ray.origin + ray.direction * 500);
        }

        if (m_heldObject != null)
        {
            if (OVRInput.GetUp(Button.PrimaryIndexTrigger) || Input.GetMouseButtonUp(0))
                OnRelease();
        }
        foreach (Interactable interact in m_interacts)
            interact.OnHeld(gameObject);
    }
}