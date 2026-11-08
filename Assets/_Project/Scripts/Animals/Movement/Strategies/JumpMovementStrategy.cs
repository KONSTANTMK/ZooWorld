using System;

namespace ZooWorld.Animals
{
    internal sealed class JumpMovementStrategy : IMovementStrategy
    {
        public const float TurnSeconds = 0.5f;

        private const float ParabolaPeakScale = 4f;

        private readonly float _distance;
        private readonly float _flightTime;
        private readonly float _flightSpeed;
        private readonly float _height;

        private float _turnElapsed;
        private float _flightElapsed;

        public float PauseSeconds { get; }
        public bool IsFinished { get; private set; }

        public JumpMovementStrategy(float distance, float interval, float flightTime, float height)
        {
            if (!IsTimingValid(interval, flightTime))
            {
                throw new ArgumentOutOfRangeException(nameof(flightTime));
            }

            _distance = distance;
            _flightTime = flightTime;
            _flightSpeed = distance / flightTime;
            _height = height;
            PauseSeconds = interval - TurnSeconds - flightTime;
        }

        public static bool IsTimingValid(float interval, float flightTime)
        {
            return flightTime > 0f && TurnSeconds + flightTime <= interval;
        }

        public void Start(AnimalMover mover)
        {
            _turnElapsed = 0f;
            _flightElapsed = 0f;
            IsFinished = false;
            mover.TurnToRandomDirection(_distance);
        }

        public void Tick(float deltaTime, AnimalMover mover)
        {
            if (_turnElapsed < TurnSeconds)
            {
                Turn(deltaTime, mover);
                return;
            }

            if (_flightElapsed >= _flightTime)
            {
                Land(mover);
                return;
            }

            Fly(deltaTime, mover);
        }

        private void Turn(float deltaTime, AnimalMover mover)
        {
            _turnElapsed += deltaTime;
            if (_turnElapsed >= TurnSeconds)
            {
                mover.CompleteTurn();
            }
        }

        private void Fly(float deltaTime, AnimalMover mover)
        {
            float progress = _flightElapsed / _flightTime;
            float step = MathF.Min(deltaTime, _flightTime - _flightElapsed);
            mover.Move(_flightSpeed * step / deltaTime);
            mover.Lift(ParabolaPeakScale * _height * progress * (1f - progress));
            _flightElapsed += step;
        }

        private void Land(AnimalMover mover)
        {
            mover.Stop();
            mover.Land();
            IsFinished = true;
        }
    }
}
