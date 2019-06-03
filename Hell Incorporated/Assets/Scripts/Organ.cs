using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Organ : MonoBehaviour
{
    private Rigidbody m_rb;
    [SerializeField]
    private GameObjectListSet m_imps;

    private void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject imp in m_imps.List)
        {
            float speed = Vector3.Magnitude(m_rb.velocity);
            if (speed > 5)
            {
                Vector3 dirToImp = imp.transform.position - transform.position;
                if (Vector3.Dot(m_rb.velocity, dirToImp) > 0.8f)
                {
                    m_rb.velocity = dirToImp.normalized * speed;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Imp"))
        {
            collision.transform.GetComponent<Imp>().Fall();
        }
    }
}
