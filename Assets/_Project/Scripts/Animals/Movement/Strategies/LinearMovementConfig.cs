using System.Collections.Generic;
using UnityEngine;
using ZooWorld.Shared;

namespace ZooWorld.Animals
{
    [CreateAssetMenu(menuName = MenuPaths.Root + "Movement/Linear", fileName = "LinearMovement")]
    public sealed class LinearMovementConfig : MovementConfig
    {
        [SerializeField]
        [Min(0f)]
        private float _speed = 3f;

        [SerializeField]
        [Min(0f)]
        private float _turnInterval = 2.5f;

        internal override IMovementStrategy Create() => new LinearMovementStrategy(_speed, _turnInterval);

        internal override IEnumerable<string> FindErrors()
        {
            if (_turnInterval <= 0f)
            {
                yield return "Turn interval must be positive.";
            }
        }
    }
}
