using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class GameEventListener<T, V, U> : MonoBehaviour where T : CustomUnityEvent<U> where V : GameEvent<T, V, U>
{
    public V gameEvent;
    public T response;

    public void Invoke(U obj)
    {
        response.Invoke(obj);
    }

    private void OnEnable()
    {
        gameEvent.AttachListener(this);
    }

    private void OnDisable()
    {
        gameEvent.DetachListener(this);
    }
}
