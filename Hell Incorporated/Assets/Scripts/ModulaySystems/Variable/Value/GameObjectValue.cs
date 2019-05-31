﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ModularSystem/Variables/GameObject")]
public class GameObjectValue : Variable<GameObject>
{
    private void OnEnable()
    {
        if (!persistent)
            value = null;
    }
}
