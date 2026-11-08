namespace ZooWorld.Animals
{
    internal interface IMovementStrategy
    {
        float PauseSeconds { get; }
        bool IsFinished { get; }

        void Start(AnimalMover mover);
        void Tick(float deltaTime, AnimalMover mover);
    }
}
