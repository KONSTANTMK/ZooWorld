namespace ZooWorld.Animals
{
    public interface IAnimalPopulation
    {
        int Count { get; }

        void Tick(float deltaTime);
    }
}
