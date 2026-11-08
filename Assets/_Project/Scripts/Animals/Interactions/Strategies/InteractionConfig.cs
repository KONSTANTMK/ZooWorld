using UnityEngine;

namespace ZooWorld.Animals
{
    public abstract class InteractionConfig : ScriptableObject
    {
        internal abstract IInteractionStrategy Create();
    }
}
