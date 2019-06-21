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
    bool m_goingToHell;

    private void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    public void Init()
    {
        //Get the imp ready to start flying to the portal
        m_isWorking = false;
        m_isFlying = true;
        m_impList.Add(gameObject);
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().useGravity = false;
        transform.rotation = Quaternion.identity;
        m_goingToHell = false;
    }

    private void OnEnable()
    {

    }

    public void EndGame()
    {
        FlyAway();
    }

    private void OnDisable()
    {
        //Remove imp from list so it isn't tracked
        m_impList.Remove(gameObject);
    }

    public void Fall()
    {
        //Make imp fall
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
        //Check if this imp is actually in the workingImps list
        for (int i = 0; i < 3; i++)
        {
            if (m_workingImpList.List[i] == gameObject)
            {
                //If so then remove it and change state so it flies to portal
                m_workingImpList.List[i] = null;
                m_isFlying = true;
                m_isWorking = false;
                m_goingToHell = true;
                return;
            }
        }
    }

    void Update()
    {
        if (!m_isFlying)
            return;

        //Move towards portal
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(-12.4f, 5, 0), m_flySpeed * Time.deltaTime);
        transform.forward = new Vector3(-12.4f, 5, 0) - transform.position;

        //Hack so I don't have to knock down imps everytime I test
#if UNITY_EDITOR
        if (!m_goingToHell && Input.GetKeyDown(KeyCode.Space))
            Fall();
#endif
    }

    private void OnTriggerEnter(Collider other)
    {
        //Destroy the imp when it passes the portal
        if (other.transform.CompareTag("Portal"))
        {
            m_impPool.DestroyObject(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //If the imp hits the ground, check for an empty spot in the working imps list and add it if there's one
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
                    //Working imps don't have physics interactions
                    m_rb.isKinematic = true;
                    m_workingImpList.List[i] = gameObject;
                    m_isWorking = true;
                    return;
                }
            }
            m_impPool.DestroyObject(gameObject);
        }
    }


}
