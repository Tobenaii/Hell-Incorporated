using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ListSet<T> : ScriptableObject
{
    [SerializeField]
    private List<T> list = new List<T>();
    public IReadOnlyList<T> List { get { return list.AsReadOnly(); } private set { } }

    private void OnDisable()
    {
        list.Clear();
    }

    public void Add(T obj)
    {
        list.Add(obj);
    }

    public void Remove(T obj)
    {
        list.Remove(obj);
    }

    public void RemoveAt(int index)
    {
        list.RemoveAt(index);
    }

    public void Insert(int index, T obj)
    {
        list.Insert(index, obj);
    }

    public void Clear()
    {
        list.Clear();
    }
}
