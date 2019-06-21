using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Login : Interactable
{
    [SerializeField]
    private GameObject m_mainMenu;
    [SerializeField]
    private Material m_mainMenuMat;
    [SerializeField]
    private GameObject m_inGame;
    [SerializeField]
    private Material m_inGameMat;
    [SerializeField]
    private GameObject m_endGame;
    [SerializeField]
    private Material m_endGameMat;
    [SerializeField]
    private Renderer m_renderer;

    [SerializeField]
    private GameEvent m_showStatsEvent;

    private void Awake()
    {
        ShowMainMenu();
    }

    public void ShowMainMenu()
    {
        HideAll();
        m_mainMenu.SetActive(true);
        m_renderer.material = m_mainMenuMat;
    }

    public void ShowInGame()
    {
        HideAll();
        m_inGame.SetActive(true);
        m_renderer.material = m_inGameMat;
    }

    public void ShowEndGame()
    {
        HideAll();
        m_endGame.SetActive(true);
        m_renderer.material = m_endGameMat;
        m_showStatsEvent.Invoke();
    }

    private void HideAll()
    {
        m_mainMenu.SetActive(false);
        m_inGame.SetActive(false);
        m_endGame.SetActive(false);
    }

    public override void OnClick(GameObject hand)
    {
        base.OnClick(hand);
    }
}
