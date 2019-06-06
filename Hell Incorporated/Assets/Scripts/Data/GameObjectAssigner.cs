using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectAssigner : MonoBehaviour
{
    [SerializeField]
    private GameObjectValue m_gameObjectValue;

    private void Awake()
    {
        m_gameObjectValue.value = gameObject;
    }
}
