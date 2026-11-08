using UnityEngine;
using ZooWorld.Shared;

namespace ZooWorld.Animals
{
    [CreateAssetMenu(menuName = MenuPaths.Root + "Interaction/Duel", fileName = "DuelInteraction")]
    public sealed class DuelInteractionConfig : InteractionConfig
    {
        internal override IInteractionStrategy Create() => new DuelInteractionStrategy(new EatInteractionStrategy());
    }
}
