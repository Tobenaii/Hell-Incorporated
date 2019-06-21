using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lineline : MonoBehaviour
{
    [SerializeField]
    private GameObjectPool m_pool;
    [SerializeField]
    private float m_lifeTime;
    private float m_timer;

    // Start is called before the first frame update
    void OnEnable()
    {
        m_timer += m_lifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        //Destroy obejct after specified time
        if (m_timer <= 0)
        {
            m_pool.DestroyObject(gameObject);
            m_timer += m_lifeTime;
            return;
        }
        m_timer -= Time.deltaTime;
    }
}
