using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static OVRInput;

public class HandPointer : MonoBehaviour
{
    private LineRenderer m_line;
    private Transform m_heldObject;
    private Transform m_originParent;

    // Start is called before the first frame update
    void Start()
    {
        m_line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
#if UNITY_EDITOR
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        transform.LookAt(ray.origin + ray.direction * 500);
#endif
        m_line.SetPosition(0, ray.origin);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000))
        {
            if (OVRInput.GetDown(Button.PrimaryIndexTrigger) || Input.GetMouseButtonDown(0))
            {
                if (hit.transform.CompareTag("Pickup"))
                {
                    m_originParent = hit.transform.parent;
                    hit.transform.SetParent(transform);
                    m_heldObject = hit.transform;
                    m_line.enabled = false;
                }
            }
            m_line.SetPosition(1, hit.point);
        }
        else
            m_line.SetPosition(1, ray.origin + ray.direction * 500);
        if (m_heldObject != null)
        {

            Vector3 direction = transform.position + transform.forward - m_heldObject.transform.position;
            m_heldObject.GetComponent<Rigidbody>().velocity = direction * 20;
            if (OVRInput.GetUp(Button.PrimaryIndexTrigger) || Input.GetMouseButtonUp(0))
            {
                m_heldObject.transform.SetParent(m_originParent);
                m_heldObject.GetComponent<Rigidbody>().AddForce(direction * 20, ForceMode.Impulse);
                m_heldObject = null;
                m_line.enabled = true;
            }
        }
    }
}
