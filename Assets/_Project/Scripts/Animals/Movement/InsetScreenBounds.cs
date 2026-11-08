using System.Numerics;
using ZooWorld.Shared;

namespace ZooWorld.Animals
{
    internal sealed class InsetScreenBounds : IScreenBounds
    {
        private readonly IScreenBounds _bounds;
        private readonly Vector2 _inset;

        public Vector2 Min => _bounds.Min + _inset;
        public Vector2 Max => _bounds.Max - _inset;

        public InsetScreenBounds(IScreenBounds bounds, float inset)
        {
            _bounds = bounds;
            _inset = new Vector2(inset);
        }
    }
}
