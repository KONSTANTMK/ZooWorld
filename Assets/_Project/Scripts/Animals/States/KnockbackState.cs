using System;
using System.Numerics;

namespace ZooWorld.Animals
{
    internal sealed class KnockbackState : IAnimalState
    {
        private const float Amplitude = 0.25f;
        private const float OscillationsPerSecond = 3f;
        private const float AngularFrequency = 2f * MathF.PI * OscillationsPerSecond;
        private const float Damping = 6f;
        private const float NoSquash = 0f;

        private readonly AnimalMover _mover;

        private float _elapsed;
        private float _duration;

        public bool IsFinished => _elapsed >= _duration;

        public KnockbackState(AnimalMover mover) => _mover = mover;

        public void Enter(Vector2 direction, float force, float seconds)
        {
            _mover.Push(direction, force);
            _elapsed = 0f;
            _duration = seconds;
        }

        public void Tick(float deltaTime)
        {
            _elapsed = _mover.IsOnScreen ? _elapsed + deltaTime : _duration;
            _mover.SetSquash(IsFinished ? NoSquash : GetSquash(_elapsed));
        }

        private static float GetSquash(float time)
        {
            return Amplitude * MathF.Sin(AngularFrequency * time) * MathF.Exp(-Damping * time);
        }
    }
}
