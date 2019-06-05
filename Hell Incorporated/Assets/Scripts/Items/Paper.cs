using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper : MonoBehaviour
{
    [SerializeField]
    private Material m_unstampedMat = null;
    [SerializeField]
    private Material m_stampedMat = null;
    [SerializeField]
    private GameObjectPool m_paperPool = null;

    [SerializeField]
    private float m_goingToHellMoveSpeed = 0;
    private Transform m_hellDoor;
    private bool m_sendingToHell;


    private void Start()
    {
        m_hellDoor = GameObject.Find("HellDoor").transform;
    }
    private void OnEnable()
    {
        GetComponent<MeshRenderer>().material = m_unstampedMat;
    }

    public void Stamp()
    {
        GetComponent<MeshRenderer>().material = m_stampedMat;
        m_sendingToHell = true;
    }

    private void Update()
    {
        if (m_sendingToHell)
        {
            transform.position = Vector3.MoveTowards(transform.position, m_hellDoor.position, m_goingToHellMoveSpeed * Time.deltaTime);
            if (transform.position == m_hellDoor.position)
            {
                m_sendingToHell = false;
                m_paperPool.DestroyObject(gameObject);
            }
        }
    }
}
