using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Imp : MonoBehaviour
{
    [SerializeField]
    private GameObjectPool m_impPool;
    [SerializeField]
    private float m_impSpeed;
    private Transform m_hellDoor;

    private void Start()
    {
        m_hellDoor = GameObject.Find("HellDoor").transform;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(m_hellDoor.position.x, transform.position.y, transform.position.z), m_impSpeed * Time.deltaTime);
        if (transform.position.x == m_hellDoor.position.x)
        {
            m_impPool.DestroyObject(gameObject);
        }
    }
}
