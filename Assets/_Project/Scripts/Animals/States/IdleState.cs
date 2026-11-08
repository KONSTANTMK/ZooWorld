namespace ZooWorld.Animals
{
    internal sealed class IdleState : IAnimalState
    {
        private readonly AnimalMover _mover;

        private float _secondsLeft;

        public bool IsFinished => _secondsLeft <= 0f;

        public IdleState(AnimalMover mover) => _mover = mover;

        public void Enter(float seconds) => _secondsLeft = seconds;

        public void Tick(float deltaTime) => _secondsLeft = _mover.IsOnScreen ? _secondsLeft - deltaTime : 0f;
    }
}
