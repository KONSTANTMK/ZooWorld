using System.Numerics;
using R3;

namespace ZooWorld.Animals
{
    internal sealed class Animal
    {
        private const float MinOffsetSquared = 0.0001f;

        private readonly AnimalMover _mover;

        private readonly AnimalStateMachine _stateMachine;

        private readonly Subject<Unit> _fed = new();
        private readonly Subject<Unit> _died = new();

        public int Id { get; }
        public AnimalRole Role { get; }
        public bool IsAlive => !_stateMachine.IsDead;
        public Vector2 Position => _mover.Position;
        public ReadOnlyReactiveProperty<Vector2> Direction => _mover.Direction;
        public ReadOnlyReactiveProperty<float> Height => _mover.Height;
        public ReadOnlyReactiveProperty<float> Squash => _mover.Squash;
        public Observable<Unit> Fed => _fed;
        public Observable<Unit> Died => _died;

        public Animal(int id, AnimalRole role, AnimalMover mover, IMovementStrategy movement)
        {
            Id = id;
            Role = role;
            _mover = mover;
            _stateMachine = new AnimalStateMachine(mover, movement);
        }

        public void Tick(float deltaTime)
        {
            if (!IsAlive)
            {
                return;
            }

            _mover.Tick(deltaTime);
            _stateMachine.Tick(deltaTime);
        }

        public void Feed() => _fed.OnNext(Unit.Default);

        public void Die()
        {
            if (!IsAlive)
            {
                return;
            }

            _stateMachine.EnterDeadState();
            _died.OnNext(Unit.Default);
        }

        public void Push(Vector2 direction, float force, float seconds)
        {
            if (!IsAlive)
            {
                return;
            }

            _stateMachine.EnterKnockbackState(direction, force, seconds);
        }

        public Vector2 GetDirectionFrom(Animal other)
        {
            Vector2 offset = Position - other.Position;
            if (offset.LengthSquared() < MinOffsetSquared)
            {
                return -_mover.Direction.CurrentValue;
            }

            return Vector2.Normalize(offset);
        }
    }
}
