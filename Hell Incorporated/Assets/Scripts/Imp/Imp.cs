using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Imp : MonoBehaviour
{
    [SerializeField]
    private GameObjectPool m_impPool = null;
    [SerializeField]
    private float m_impSpeed = 0;
    [SerializeField]
    private GameObjectListSet m_impList = null;
    [SerializeField]
    private GameObjectListSet m_workingImpList = null;
    private Transform m_hellDoor;
    private bool m_isFlying;
    [SerializeField]
    private AnimState m_flyToHellAnim;
    [SerializeField]
    private AnimState m_scaredToHellAnim;
    private AnimState m_state;
    public bool IsWorking => m_isWorking;
    private bool m_isWorking;

    private void Start()
    {
        m_hellDoor = GameObject.Find("HellDoor").transform;
    }

    public void SetWorking()
    {
        m_isWorking = true;
    }

    private void OnEnable()
    {
        m_isWorking = false;
        m_isFlying = true;
        m_impList.Add(gameObject);
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        GetComponent<Rigidbody>().isKinematic = false;
        transform.rotation = Quaternion.identity;
        m_state = m_flyToHellAnim;
        if (m_workingImpList.Containts(gameObject))
            return;
        if (m_workingImpList.List.Count < 3)
        {
            Fall();
        }
        m_state.Init(transform);
    }

    private void OnDisable()
    {
        m_impList.Remove(gameObject);
    }

    public void Fall()
    {
        GetComponent<Rigidbody>().useGravity = true;
        m_isFlying = false;
    }

    public void FlyAway()
    {
        m_state = m_scaredToHellAnim;
        m_state.Init(transform);
        m_workingImpList.Remove(gameObject);
        m_isFlying = true;
        m_isWorking = false;
    }

    void Update()
    {
        if (!m_isFlying)
            return;

        if (m_state.UpdateAnim(transform))
            m_impPool.DestroyObject(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            if (m_workingImpList.Containts(gameObject))
                return;
            if (m_workingImpList.List.Count < 3)
                m_workingImpList.Add(gameObject);
        }
    }
}
