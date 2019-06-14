using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueThingy
{
    [SerializeField]
    private bool m_closeOnLastPage = true;
    [SerializeField]
    private string m_dialogue;

    public string Dialogue { get { return m_dialogue; } private set { } }
    public bool CloseOnLastPatge { get { return m_closeOnLastPage; } private set { } }

    public DialogueThingy(string d)
    {
        m_dialogue = d;
    }
}


[CreateAssetMenu(menuName = "Dialogue/ListFile")]
[System.Serializable]
public class DialogueList : ScriptableObject
{
    public int Count => m_dialogues.Count;

    private enum LineType { LineId, LineStart, LineDialogue};


    [SerializeField]
    private TextAsset m_dialogueFile = null;
    [SerializeField]
    private List<DialogueThingy> m_dialogues = new List<DialogueThingy>();

    public void LoadDialogueFile()
    {
        m_dialogues.Clear();

        LineType lineType = LineType.LineId;
        string id = "";

        string dialogue = "";
        bool newLine = false;
        foreach (string line in m_dialogueFile.text.Split(new char[] { '\r', '\n' }))
        {
            if (newLine)
            {
                newLine = false;
                continue;
            }
            if (line == "/page")
            {
                dialogue += " /page ";
                newLine = true;
                continue;
            }
            newLine = true;
            switch (lineType)
            {
                case LineType.LineId:
                    id = line;
                    lineType = LineType.LineStart;
                    break;
                case LineType.LineStart:
                    lineType = LineType.LineDialogue;
                    break;
                case LineType.LineDialogue:
                    if (line == "/break")
                    {
                        m_dialogues.Add(new DialogueThingy(dialogue));
                        Debug.Log("Loaded dialogue: " + id);
                        lineType = LineType.LineId;
                        dialogue = "";
                        break;
                    }
                    dialogue += line;
                    break;
            }   
        }
#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }

    public DialogueThingy GetDialogue(int index)
    {
        if (index < 0 || index >= m_dialogues.Count)
            return null;
        return m_dialogues[index];
    }
}
