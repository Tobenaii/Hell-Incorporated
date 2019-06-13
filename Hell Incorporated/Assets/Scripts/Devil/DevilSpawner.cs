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
    private bool m_canSpawn;

    // Start is called before the first frame update
    void Start()
    {
        m_devilInstance = Instantiate(m_devilPrefab, transform);
        m_devilInstance.SetActive(false);
        m_timer = 1;
        m_canSpawn = false;
    }

    public void StartDevil()
    {
        m_canSpawn = true;
    }
    void SpawnDevil()
    {
        m_devilInstance.transform.localPosition = Vector3.zero;
        m_devilInstance.transform.localRotation = Quaternion.identity;
        m_devilInstance.SetActive(true);
        m_devilInstance.GetComponent<Devil>().Init();
        m_timer = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_canSpawn)
            return;
        m_devilInScene = m_devilInstance.activeSelf;

        if (m_devilInScene)
            return;

        m_timer -= Time.deltaTime;
        if (m_timer <= 0)
        {
            float spawn = Random.Range(0, 100);
            if (spawn < m_spawnChancePerSecond)
                SpawnDevil();
        }
    }
}
