namespace ZooWorld.Animals
{
    internal interface IAnimalState
    {
        bool IsFinished { get; }

        void Tick(float deltaTime);
    }
}
