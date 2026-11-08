using System.Collections.Generic;
using UnityEngine;

namespace ZooWorld.Animals
{
    public abstract class MovementConfig : ScriptableObject
    {
        internal abstract IMovementStrategy Create();
        internal abstract IEnumerable<string> FindErrors();
    }
}
