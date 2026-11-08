using UnityEngine;
using ZooWorld.Shared;

namespace ZooWorld.Animals
{
    [CreateAssetMenu(menuName = MenuPaths.Root + "Interaction/Bounce", fileName = "BounceInteraction")]
    public sealed class BounceInteractionConfig : InteractionConfig
    {
        [SerializeField]
        [Min(0f)]
        private float _force = 4f;

        [SerializeField]
        [Min(0f)]
        private float _pushSeconds = 0.5f;

        internal override IInteractionStrategy Create() => new BounceInteractionStrategy(_force, _pushSeconds);
    }
}
