using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DialogueList))]
public class DialogueListEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DialogueList dList = (DialogueList)target;
        if (GUILayout.Button("Load Dialogue"))
        {
            dList.LoadDialogueFile();
        }
    }
}
