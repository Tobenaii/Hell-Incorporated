using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Login : MonoBehaviour
{
    [SerializeField]
    private PlayerProcessor m_playerProc;
    [SerializeField]
    private GameObject m_tutorial;
    [SerializeField]
    private GameObject m_gameBeats;
    [SerializeField]
    private GameObject m_screen;
    [SerializeField]
    private Material m_loginMat;
    [SerializeField]
    private Material m_processingMat;

    public void StartGame()
    {
        m_playerProc.InitPlayer();
        m_screen.GetComponent<Renderer>().material = m_processingMat;
        Instantiate(m_gameBeats);
        Instantiate(m_tutorial);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
