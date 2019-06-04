using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    [SerializeField]
    private float m_rotationSpeed;
    [SerializeField]
    private FloatEvent m_scanEvent;
    [SerializeField]
    private ProcState m_procState;
    private bool m_autoScanning;

    private void Start()
    {
        m_procState.state = ProcState.ProcessorState.Scan;
    }

    public void SetAutoScan()
    {
        m_autoScanning = true;
    }

    public void Scan()
    {
        m_scanEvent.Invoke(0.0f);
        m_procState.state = ProcState.ProcessorState.Type;
    }

    private void OnTriggerStay(Collider other)
    {
        if (m_autoScanning)
            return;
        if (m_procState.state != ProcState.ProcessorState.Scan)
            return;
        if (other.transform.CompareTag("Paper"))
        {
            Quaternion q = Quaternion.Euler(180, 0, -90);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, m_rotationSpeed);
            if (transform.rotation == q)
            {
                Scan();
            }
        }
    }
}
