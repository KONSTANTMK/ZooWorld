using System.Numerics;

namespace ZooWorld.Animals
{
    internal interface IAnimalSpawner
    {
        void Spawn(AnimalDefinition definition, Vector2 position);
    }
}
