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
    [SerializeField]
    private GameObjectValue m_hellDoor;
    private bool m_sendingToHell;

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
            transform.position = Vector3.MoveTowards(transform.position, m_hellDoor.value.transform.position, m_goingToHellMoveSpeed * Time.deltaTime);
            if (transform.position == m_hellDoor.value.transform.position)
            {
                m_sendingToHell = false;
                m_paperPool.DestroyObject(gameObject);
            }
        }
    }
}
