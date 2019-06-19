using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Imp : Interactable
{
    [SerializeField]
    private float m_flySpeed;
    [SerializeField]
    private GameObjectPool m_impPool = null;
    [SerializeField]
    private float m_impSpeed = 0;
    [SerializeField]
    private GameObjectListSet m_impList = null;
    [SerializeField]
    private GameObjectListSet m_workingImpList = null;

    private bool m_isFlying;
    private Rigidbody m_rb;

    public bool IsWorking => m_isWorking;
    private bool m_isWorking;

    private void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    public void Init()
    {
        m_isWorking = false;
        m_isFlying = true;
        m_impList.Add(gameObject);
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().useGravity = false;
        transform.rotation = Quaternion.identity;
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {
        m_impList.Remove(gameObject);
    }

    public void Fall()
    {
        GetComponent<Rigidbody>().useGravity = true;
        m_impList.Remove(gameObject);
        m_isFlying = false;
    }

    public override void OnClick(GameObject hand)
    {
        FlyAway();
    }

    public void FlyAway()
    {
        if (!m_isWorking)
            return;
        for (int i = 0; i < 3; i++)
        {
            if (m_workingImpList.List[i] == gameObject)
            {
                m_workingImpList.List[i] = null;
                break;
            }
        }
        m_isFlying = true;
        m_isWorking = false;
    }

    void Update()
    {
        if (!m_isFlying)
            return;

        transform.position += Vector3.left * m_flySpeed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
            Fall();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Portal"))
            m_impPool.DestroyObject(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            if (m_workingImpList.Containts(gameObject))
                return;
            while (m_workingImpList.List.Count < 3)
                m_workingImpList.Add(null);
            for (int i = 0; i < 3; i++)
            {
                if (!m_workingImpList.List[i])
                {
                    m_rb.isKinematic = true;
                    m_workingImpList.List[i] = gameObject;
                    m_isWorking = true;
                    return;
                }
            }
        }
    }


}
