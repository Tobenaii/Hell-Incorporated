using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Imp : MonoBehaviour
{
    [SerializeField]
    private GameObjectPool m_impPool;
    [SerializeField]
    private float m_impSpeed;
    [SerializeField]
    private GameObjectListSet m_impList;
    private Transform m_hellDoor;
    private bool m_isFlying;

    private void Start()
    {
        m_hellDoor = GameObject.Find("HellDoor").transform;
    }

    private void OnEnable()
    {
        m_isFlying = true;
        m_impList.Add(gameObject);
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }

    private void OnDisable()
    {
        m_impList.Remove(gameObject);
    }

    public void Fall()
    {
        GetComponent<Rigidbody>().useGravity = true;
        m_isFlying = false;
        m_impList.Remove(gameObject);
    }

    void Update()
    {
        if (!m_isFlying)
            return;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(m_hellDoor.position.x, transform.position.y, transform.position.z), m_impSpeed * Time.deltaTime);
        if (transform.position.x == m_hellDoor.position.x)
        {
            m_impPool.DestroyObject(gameObject);
        }
    }
}
