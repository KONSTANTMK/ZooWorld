using System;
using System.Numerics;
using R3;
using ZooWorld.Shared;

namespace ZooWorld.Animals
{
    internal sealed class AnimalDirection
    {
        private const float FullCircle = 2f * MathF.PI;
        private const float WanderAngle = MathF.PI / 2f;
        private const float EdgeDistance = 1.5f;
        private const float TurnSpeed = 3.5f;

        private readonly IAnimalBodyComponent _body;
        private readonly IScreenBounds _bounds;
        private readonly IScreenBounds _edgeZone;
        private readonly IRandom _random;

        private readonly ReactiveProperty<Vector2> _current = new();

        private Vector2 _target;

        public ReadOnlyReactiveProperty<Vector2> Current => _current;

        public AnimalDirection(IAnimalBodyComponent body, IScreenBounds bounds, IRandom random)
        {
            _body = body;
            _bounds = bounds;
            _edgeZone = new InsetScreenBounds(bounds, EdgeDistance);
            _random = random;

            Vector2 randomDirection = Rotate(Vector2.UnitX, _random.Range(0f, FullCircle));
            _target = KeepInside(randomDirection);
            _current.Value = _target;
        }

        public void Tick(float deltaTime)
        {
            ReturnToScreen();
            _current.Value = RotateTowards(_current.Value, _target, TurnSpeed * deltaTime);
        }

        public void TurnToRandom() => _target = GetWanderDirection();

        public void TurnToRandom(float reach)
        {
            Vector2 direction = GetWanderDirection();
            Vector2 landing = _body.Position + direction * reach;
            _target = _bounds.TryReflectInward(landing, direction, out Vector2 reflected) ? reflected : direction;
        }

        public void CompleteTurn() => _current.Value = _target;

        public void Redirect(Vector2 direction)
        {
            _target = direction;
            _current.Value = direction;
        }

        private Vector2 GetWanderDirection()
        {
            Vector2 direction = Rotate(_current.Value, _random.Range(-WanderAngle, WanderAngle));
            return KeepInside(direction);
        }

        private void ReturnToScreen()
        {
            Vector2 position = _body.Position;
            if (_edgeZone.Contains(position))
            {
                return;
            }

            _target = KeepInside(_target, position);
            if (!_bounds.Contains(position))
            {
                _current.Value = _target;
            }
        }

        private Vector2 KeepInside(Vector2 direction) => KeepInside(direction, _body.Position);

        private Vector2 KeepInside(Vector2 direction, Vector2 position)
        {
            return _edgeZone.TryReflectInward(position, direction, out Vector2 reflected) ? reflected : direction;
        }

        private static Vector2 Rotate(Vector2 direction, float radians)
        {
            return Vector2.Transform(direction, Matrix3x2.CreateRotation(radians));
        }

        private static Vector2 RotateTowards(Vector2 from, Vector2 to, float maxRadians)
        {
            float angle = MathF.Atan2(from.X * to.Y - from.Y * to.X, Vector2.Dot(from, to));
            float step = Math.Clamp(angle, -maxRadians, maxRadians);
            return Vector2.Normalize(Rotate(from, step));
        }
    }
}
