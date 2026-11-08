using System.Numerics;
using R3;
using ZooWorld.Shared;

namespace ZooWorld.Animals
{
    internal sealed class AnimalMover
    {
        private const float GroundHeight = 0f;

        private readonly IAnimalBodyComponent _body;
        private readonly IScreenBounds _bounds;

        private readonly AnimalDirection _direction;

        private readonly ReactiveProperty<float> _height = new();
        private readonly ReactiveProperty<float> _squash = new();

        public ReadOnlyReactiveProperty<Vector2> Direction => _direction.Current;
        public ReadOnlyReactiveProperty<float> Height => _height;
        public ReadOnlyReactiveProperty<float> Squash => _squash;
        public Vector2 Position => _body.Position;
        public bool IsOnScreen => _bounds.Contains(_body.Position);

        public AnimalMover(IAnimalBodyComponent body, IScreenBounds bounds, IRandom random)
        {
            _body = body;
            _bounds = bounds;
            _direction = new AnimalDirection(body, bounds, random);
        }

        public void Tick(float deltaTime)
        {
            if (_height.Value > GroundHeight)
            {
                return;
            }

            _direction.Tick(deltaTime);
        }

        public void TurnToRandomDirection() => _direction.TurnToRandom();
        public void TurnToRandomDirection(float reach) => _direction.TurnToRandom(reach);
        public void CompleteTurn() => _direction.CompleteTurn();

        public void Move(float speed) => _body.SetVelocity(_direction.Current.CurrentValue * speed);
        public void Stop() => _body.SetVelocity(Vector2.Zero);
        public void DisableCollisions() => _body.DisableCollisions();

        public void Push(Vector2 direction, float force)
        {
            Land();
            _direction.Redirect(direction);
            _body.AddImpulse(direction * force);
        }

        public void Lift(float height) => _height.Value = height;
        public void Land() => _height.Value = GroundHeight;

        public void SetSquash(float squash) => _squash.Value = squash;
    }
}
