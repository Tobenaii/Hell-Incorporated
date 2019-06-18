using System.Collections;
using System.Collections.Generic;
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

    private void Awake()
    {
        m_welcomeMaterial = m_screenRenderer.material;
    }

    public override void OnClick(GameObject hand)
    {
        base.OnClick(hand);
        m_screenRenderer.material = m_gameScreenMaterial;
        m_scanDialogueEvent.Invoke();
    }
}
