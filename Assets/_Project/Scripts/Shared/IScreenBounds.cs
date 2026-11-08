using System.Numerics;

namespace ZooWorld.Shared
{
    public interface IScreenBounds
    {
        Vector2 Min { get; }
        Vector2 Max { get; }
    }
}
