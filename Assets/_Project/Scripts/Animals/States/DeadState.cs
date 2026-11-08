using System;

namespace ZooWorld.Animals
{
    internal sealed class DeadState : IAnimalState
    {
        private readonly AnimalMover _mover;

        public bool IsFinished => false;

        public DeadState(AnimalMover mover) => _mover = mover;

        public void Enter()
        {
            _mover.Stop();
            _mover.DisableCollisions();
        }

        public void Tick(float deltaTime) => throw new InvalidOperationException("A dead animal must not tick.");
    }
}
