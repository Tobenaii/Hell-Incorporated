using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Login : Interactable
{
    [SerializeField]
    private GameEvent m_scanDialogueEvent;
    [SerializeField]
    private Renderer m_screenRenderer;
    [SerializeField]
    private Material m_gameScreenMaterial;
    private Material m_welcomeMaterial;
    [SerializeField]
    private Material m_performanceStatsMaterial;
    [SerializeField]
    private TextMeshPro m_statsText;
    [SerializeField]
    private DigitalClock m_clock;
    [SerializeField]
    private GameObject m_continueButton;

    private void Awake()
    {
        m_welcomeMaterial = m_screenRenderer.material;
        m_continueButton.SetActive(false);
    }

    public void Restart()
    {
        m_continueButton.SetActive(false);
        m_screenRenderer.material = m_gameScreenMaterial;
        m_statsText.text = "";
    }

    public override void OnClick(GameObject hand)
    {
        base.OnClick(hand);
        m_screenRenderer.material = m_gameScreenMaterial;
        m_scanDialogueEvent.Invoke();
    }

    public void ShowPerformanceStats()
    {
        m_continueButton.SetActive(true);
        m_screenRenderer.material = m_performanceStatsMaterial;
        m_statsText.text = "You have finished your shift at " + m_clock.GetDigitalTime();
    }
}
