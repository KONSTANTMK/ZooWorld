using System;

namespace ZooWorld.Animals
{
    internal sealed class LinearMovementStrategy : IMovementStrategy
    {
        private readonly float _speed;
        private readonly float _turnInterval;

        private float _timeSinceTurn;

        public float PauseSeconds => 0f;
        public bool IsFinished => false;

        public LinearMovementStrategy(float speed, float turnInterval)
        {
            if (turnInterval <= 0f)
            {
                throw new ArgumentOutOfRangeException(nameof(turnInterval));
            }

            _speed = speed;
            _turnInterval = turnInterval;
        }

        public void Start(AnimalMover mover) => _timeSinceTurn = 0f;

        public void Tick(float deltaTime, AnimalMover mover)
        {
            _timeSinceTurn += deltaTime;

            if (_timeSinceTurn >= _turnInterval)
            {
                _timeSinceTurn -= _turnInterval;
                mover.TurnToRandomDirection();
            }

            mover.Move(_speed);
        }
    }
}
