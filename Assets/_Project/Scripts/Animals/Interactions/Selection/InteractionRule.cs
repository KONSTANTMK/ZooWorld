using System;
using UnityEngine;

namespace ZooWorld.Animals
{
    [Serializable]
    internal sealed class InteractionRule
    {
        [SerializeField]
        private AnimalRole _actorRole;

        [SerializeField]
        private AnimalRole _targetRole;

        [SerializeField]
        private InteractionConfig _interaction;

        public bool HasRoles(AnimalRole actor, AnimalRole target) => _actorRole == actor && _targetRole == target;

        public IInteractionStrategy CreateStrategy() => _interaction.Create();
    }
}
