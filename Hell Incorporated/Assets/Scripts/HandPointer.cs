using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static OVRInput;

public class HandPointer : MonoBehaviour
{
    [SerializeField]
    private FloatEvent m_keyboardEvent = null;
    private LineRenderer m_line;
    private Transform m_heldObject;
    private Transform m_originParent;
    private Vector3 m_prevObjPos;
    private bool m_keyboardCheck;

    // Start is called before the first frame update
    void Start()
    {
        m_line = GetComponent<LineRenderer>();
        m_keyboardCheck = false;
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
        if (Physics.Raycast(ray, out hit, 1000, 1 << 9))
        {
            if (hit.transform.CompareTag("Keyboard"))
            {
                if (!m_keyboardCheck)
                {
                    m_keyboardEvent.Invoke(0.0f);
                    m_keyboardCheck = true;
                }
            }
            if (m_heldObject == null && OVRInput.GetDown(Button.PrimaryIndexTrigger) || Input.GetMouseButtonDown(0))
            {
                m_originParent = hit.transform.parent;
                hit.transform.SetParent(transform);
                hit.transform.GetComponent<Rigidbody>().freezeRotation = true;
                m_heldObject = hit.transform;
                m_line.enabled = false;
            }
            m_line.SetPosition(1, hit.point);
        }
        else
        {
            m_line.SetPosition(1, ray.origin + ray.direction * 500);
            m_keyboardCheck = false;
        }
        if (m_heldObject != null)
        {
            Vector3 direction = transform.position + transform.forward * 0.2f - m_heldObject.transform.position;

#if UNITY_EDITOR
            direction = transform.position + transform.forward * 0.5f - m_heldObject.transform.position;
#endif
            m_heldObject.GetComponent<Rigidbody>().velocity = direction * 10;
            if (OVRInput.GetUp(Button.PrimaryIndexTrigger) || Input.GetMouseButtonUp(0))
            {
                m_heldObject.transform.parent = m_originParent;
                m_heldObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                direction = m_heldObject.transform.position - m_prevObjPos;
                m_heldObject.GetComponent<Rigidbody>().AddForce(transform.forward * 70 * Vector3.Magnitude(direction), ForceMode.Impulse);
                m_heldObject.GetComponent<Rigidbody>().freezeRotation = false;
                m_heldObject = null;
                m_line.enabled = true;
            }
        }
        if (m_heldObject != null)
            m_prevObjPos = m_heldObject.transform.position;
    }
}
