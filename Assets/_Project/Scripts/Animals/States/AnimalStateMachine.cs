using System.Numerics;

namespace ZooWorld.Animals
{
    internal sealed class AnimalStateMachine
    {
        private readonly MovingState _moving;
        private readonly IdleState _idle;
        private readonly KnockbackState _knockback;
        private readonly DeadState _dead;

        private IAnimalState _current;

        public bool IsDead => _current == _dead;

        public AnimalStateMachine(AnimalMover mover, IMovementStrategy movement)
        {
            _moving = new MovingState(mover, movement);
            _idle = new IdleState(mover);
            _knockback = new KnockbackState(mover);
            _dead = new DeadState(mover);
            EnterIdleState(_moving.PauseSeconds);
        }

        public void Tick(float deltaTime)
        {
            _current.Tick(deltaTime);
            if (!_current.IsFinished)
            {
                return;
            }

            if (_current == _moving)
            {
                EnterIdleState(_moving.PauseSeconds);
            }
            else
            {
                EnterMovingState();
            }
        }

        public void EnterKnockbackState(Vector2 direction, float force, float seconds)
        {
            _knockback.Enter(direction, force, seconds);
            _current = _knockback;
        }

        public void EnterDeadState()
        {
            _dead.Enter();
            _current = _dead;
        }

        private void EnterMovingState()
        {
            _moving.Enter();
            _current = _moving;
        }

        private void EnterIdleState(float seconds)
        {
            _idle.Enter(seconds);
            _current = _idle;
        }
    }
}
