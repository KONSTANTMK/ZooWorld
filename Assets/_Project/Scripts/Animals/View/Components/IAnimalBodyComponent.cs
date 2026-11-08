using System.Numerics;

namespace ZooWorld.Animals
{
    internal interface IAnimalBodyComponent
    {
        Vector2 Position { get; }

        void SetVelocity(Vector2 velocity);
        void AddImpulse(Vector2 impulse);
        void DisableCollisions();
    }
}
