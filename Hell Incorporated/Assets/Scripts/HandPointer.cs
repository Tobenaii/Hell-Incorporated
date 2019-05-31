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
        m_line.SetPosition(0, ray.origin);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000))
        {
            if (Input.GetButtonDown("Oculus_CrossPlatform_PrimaryIndexTrigger"))
            {
                m_originParent = hit.transform.parent;
                hit.transform.SetParent(transform);
                m_heldObject = hit.transform;
            }
            if (Input.GetAxis("Oculus_CrossPlatform_PrimaryIndexTrigger") != 0)
            {
                m_heldObject.transform.SetParent(m_originParent);
            }
            m_line.SetPosition(1, hit.point);
        }
        else
            m_line.SetPosition(1, ray.origin + ray.direction * 500);

    }
}
