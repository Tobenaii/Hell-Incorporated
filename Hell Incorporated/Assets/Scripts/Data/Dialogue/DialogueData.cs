using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/DialogueData")]
[System.Serializable]
public class DialogueData : ScriptableObject
{
    [SerializeField]
    private TextAsset m_dialogueFile = null;
    [SerializeField]
    private bool m_closeOnLastPage = true;

    public bool CloseOnLastPage { get { return m_closeOnLastPage; } private set { } }
    public string Dialogue { get { return m_dialogueFile.text; } private set { } }
}
