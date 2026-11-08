namespace ZooWorld.Animals
{
    internal sealed class MovingState : IAnimalState
    {
        private readonly AnimalMover _mover;
        private readonly IMovementStrategy _movement;

        public bool IsFinished => _movement.IsFinished;
        public float PauseSeconds => _movement.PauseSeconds;

        public MovingState(AnimalMover mover, IMovementStrategy movement)
        {
            _mover = mover;
            _movement = movement;
        }

        public void Enter()
        {
            _mover.Stop();
            _movement.Start(_mover);
        }

        public void Tick(float deltaTime) => _movement.Tick(deltaTime, _mover);
    }
}
