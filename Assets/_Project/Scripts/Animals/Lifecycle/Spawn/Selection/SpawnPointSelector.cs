using UnityEngine;
using ZooWorld.Shared;
using Vector2 = System.Numerics.Vector2;

namespace ZooWorld.Animals
{
    internal sealed class SpawnPointSelector
    {
        private const float ProbeHeight = 0.5f;

        private readonly IScreenBounds _bounds;
        private readonly IRandom _random;
        private readonly SpawnSettings _settings;

        public SpawnPointSelector(IScreenBounds bounds, IRandom random, SpawnSettings settings)
        {
            _bounds = bounds;
            _random = random;
            _settings = settings;
        }

        public Vector2 Select() => TryFindFreePoint(out Vector2 point) ? point : GetRandomPoint();

        private bool TryFindFreePoint(out Vector2 point)
        {
            for (var attempt = 0; attempt < _settings.MaxAttempts; attempt++)
            {
                point = GetRandomPoint();
                if (!IsOccupied(point))
                {
                    return true;
                }
            }

            point = default;
            return false;
        }

        private Vector2 GetRandomPoint()
        {
            float margin = _settings.EdgeMargin;
            return new Vector2(
                _random.Range(_bounds.Min.X + margin, _bounds.Max.X - margin),
                _random.Range(_bounds.Min.Y + margin, _bounds.Max.Y - margin));
        }

        private bool IsOccupied(Vector2 point)
        {
            var center = new Vector3(point.X, ProbeHeight, point.Y);
            return Physics.CheckSphere(
                center,
                _settings.FreeRadius,
                _settings.AnimalLayers,
                QueryTriggerInteraction.Ignore);
        }
    }
}
