using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Variable<T> : ScriptableObject
{
    [SerializeField]
    protected T value;
    [SerializeField]
    protected bool persistent;

    public T Value => value;
}
