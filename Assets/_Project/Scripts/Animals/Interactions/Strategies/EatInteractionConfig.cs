using UnityEngine;
using ZooWorld.Shared;

namespace ZooWorld.Animals
{
    [CreateAssetMenu(menuName = MenuPaths.Root + "Interaction/Eat", fileName = "EatInteraction")]
    public sealed class EatInteractionConfig : InteractionConfig
    {
        internal override IInteractionStrategy Create() => new EatInteractionStrategy();
    }
}
