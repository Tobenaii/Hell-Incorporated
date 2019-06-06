using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimState : ScriptableObject
{
    public abstract void Init(Transform transform);

    public abstract bool UpdateAnim(Transform transform);
}
