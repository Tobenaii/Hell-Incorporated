using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static OVRInput;

public class HandPointer : MonoBehaviour
{
    [SerializeField]
    private GameEvent m_keyboardEvent = null;
    [SerializeField]
    private GameObjectListSet m_workingImpList;
    [SerializeField]
    private GameObjectPool m_organPool;
    [SerializeField]
    private AudioSource m_typeSound;
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

    private void PickupObject(GameObject obj)
    {
        m_originParent = obj.transform.parent;
        obj.transform.SetParent(transform);
        obj.transform.GetComponent<Rigidbody>().freezeRotation = true;
        m_heldObject = obj.transform;
        m_line.enabled = false;
    }

    public void LetGo()
    {
        m_heldObject.transform.parent = m_originParent;
        m_heldObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        m_heldObject = null;
        m_line.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position - transform.forward * 0.1f, transform.forward);
#if UNITY_EDITOR
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        transform.LookAt((ray.origin - transform.forward * 0.1f) + ray.direction * 500);
#endif
        m_line.SetPosition(0, ray.origin);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000, 1 << 9))
        {
            hit.transform.GetComponent<BoundItem>()?.Held(true);

            if (hit.transform.CompareTag("Keyboard"))
            {
                if (!m_keyboardCheck)
                {
                    m_keyboardEvent.Invoke();
                    m_keyboardCheck = true;
                    m_typeSound.Play();
                }
            }

            else if (m_heldObject == null && OVRInput.GetDown(Button.PrimaryIndexTrigger) || Input.GetMouseButtonDown(0))
            {
                if (hit.transform.CompareTag("Imp"))
                {
                    if (m_workingImpList.Containts(hit.transform.gameObject))
                    {
                        hit.transform.GetComponent<Imp>().FlyAway();
                    }
                }
                else if (hit.transform.CompareTag("OrganSpawn"))
                {
                    GameObject organ = m_organPool.GetObject();
                    organ.transform.position = hit.transform.position;
                    organ.transform.SetParent(GameObject.Find("Fix").transform);
                    PickupObject(organ);
                }
                else if (hit.transform.CompareTag("Dialogue"))
                {
                    hit.transform.GetComponent<Dialogue>().OnClick();
                }
                else
                {
                    PickupObject(hit.transform.gameObject);
                }
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
                BoundItem item = m_heldObject.GetComponent<BoundItem>();
                if (item != null)
                {
                    item.Dissolve();
                    item.Held(false);
                }
                direction = m_heldObject.transform.position - m_prevObjPos;
                float force = 40 * Vector3.Magnitude(direction);
                if (force > 200)
                    force = 200;
                m_heldObject.GetComponent<Rigidbody>().AddForce(Vector3.Normalize(transform.up * -1 + transform.forward) * force, ForceMode.Impulse);
                m_heldObject.GetComponent<Rigidbody>().freezeRotation = false;
                LetGo();
            }
        }
        if (m_heldObject != null)
            m_prevObjPos = m_heldObject.transform.position;
    }
}
