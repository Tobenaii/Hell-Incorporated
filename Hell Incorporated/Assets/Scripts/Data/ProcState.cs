using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Processor/State")]
public class ProcState : ScriptableObject
{
    public enum ProcessorState {Scan, Type, Stamp};

    public ProcessorState state;
}
