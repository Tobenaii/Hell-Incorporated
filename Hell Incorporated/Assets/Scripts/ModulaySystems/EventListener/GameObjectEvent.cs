using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/GameObject")]
public class GameObjectEvent : GameEvent<CustomGameObjectEvent, GameObjectEvent, GameObject>
{

}
