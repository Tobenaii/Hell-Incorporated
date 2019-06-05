using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilSpawner : MonoBehaviour
{
    [SerializeField]
    private float m_spawnChancePerSecond = 0;
    [SerializeField]
    private GameObject m_devilPrefab;
    private GameObject m_devilInstance;
    private bool m_devilInScene;
    private float m_timer;

    // Start is called before the first frame update
    void Start()
    {
        m_devilInstance = Instantiate(m_devilPrefab, transform);
        m_devilInstance.SetActive(false);
        m_timer = 1;
    }

    void SpawnDevil()
    {
        m_devilInstance.SetActive(true);
        m_devilInstance.transform.localPosition = Vector3.zero;
        m_devilInstance.transform.localRotation = Quaternion.identity;
        m_devilInScene = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_devilInScene)
        {
            if (m_devilInstance.activeSelf)
                m_devilInScene = false;
            return;
        }

        if (m_timer <= 0)
        {
            m_timer = 1;
            float spawn = Random.Range(0, 100);
            if (spawn < m_spawnChancePerSecond)
                SpawnDevil();
        }
        else if (!m_devilInScene)
            m_timer -= Time.deltaTime;
    }
}
