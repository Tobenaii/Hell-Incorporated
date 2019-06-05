using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/ListFile")]
public class DialogueList : ScriptableObject
{
    public int Count => m_dialogues.Count;

    private enum LineType { LineId, LineStart, LineDialogue};

    [SerializeField]
    private TextAsset m_dialogueFile = null;
    [SerializeField]
    private List<string> m_dialogues = new List<string>();

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
                        m_dialogues.Add(dialogue);
                        Debug.Log("Loaded dialogue: " + id);
                        lineType = LineType.LineId;
                        dialogue = "";
                        break;
                    }
                    dialogue += line;
                    break;
                
            }   
        }
    }

    public string GetDialogue(int index)
    {
        if (index < 0 || index >= m_dialogues.Count)
            return "Index out of range";
        return m_dialogues[index];
    }
}
