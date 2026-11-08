using System.Collections.Generic;
using UnityEngine;
using ZooWorld.Shared;

namespace ZooWorld.Animals
{
    [CreateAssetMenu(menuName = MenuPaths.Root + "Movement/Jump", fileName = "JumpMovement")]
    public sealed class JumpMovementConfig : MovementConfig
    {
        [SerializeField]
        [Min(0f)]
        private float _distance = 2f;

        [SerializeField]
        [Min(0f)]
        private float _interval = 1.5f;

        [SerializeField]
        [Min(0f)]
        private float _flightTime = 0.4f;

        [SerializeField]
        [Min(0f)]
        private float _height = 0.6f;

        internal override IMovementStrategy Create()
        {
            return new JumpMovementStrategy(_distance, _interval, _flightTime, _height);
        }

        internal override IEnumerable<string> FindErrors()
        {
            if (!JumpMovementStrategy.IsTimingValid(_interval, _flightTime))
            {
                yield return "Jump flight time must be positive and fit into the interval with the turn.";
            }
        }
    }
}
