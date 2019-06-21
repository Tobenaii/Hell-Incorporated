using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Organ : MonoBehaviour
{
    private Rigidbody m_rb;
    [SerializeField]
    private GameObjectListSet m_imps = null;

    private void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Loop through every flying imp in scene
        foreach (GameObject imp in m_imps.List)
        {
            //Store velocity of organ for later
            float speed = Vector3.Magnitude(m_rb.velocity);
            if (speed > 5)
            {
                //Check if the direction of organ velocity is similiar to the direction from the organ to the imp using dot product
                Vector3 dirToImp = imp.transform.position - transform.position;
                if (Vector3.Dot(dirToImp.normalized, m_rb.velocity.normalized) > 0.85f)
                {
                    //If the directions are similiar, lock on to imp
                    m_rb.velocity = dirToImp.normalized * speed;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Make imp fall if organ hits it
        if (collision.transform.CompareTag("Imp"))
        {
            collision.transform.GetComponent<Imp>().Fall();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Distract AI processors if organ hits them
        if (other.transform.CompareTag("AiProcessor"))
            other.transform.GetComponent<AiProcessor>().Distract();
    }
}
