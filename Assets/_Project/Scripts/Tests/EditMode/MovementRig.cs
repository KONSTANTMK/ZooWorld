using ZooWorld.Animals;

namespace ZooWorld.Tests
{
    internal sealed class MovementRig
    {
        public const float Step = 0.02f;
        private const int AnimalId = 1;

        private readonly Animal _animal;

        public FakeBody Body { get; } = new();
        public AnimalMover Mover { get; }

        public MovementRig(IMovementStrategy movement, FakeRandom random)
        {
            Mover = new AnimalMover(Body, new FakeBounds(), random);
            _animal = new Animal(AnimalId, AnimalRole.Prey, Mover, movement);
        }

        public void Simulate(float seconds)
        {
            for (var time = 0f; time < seconds; time += Step)
            {
                _animal.Tick(Step);
                Body.Integrate(Step);
            }
        }
    }
}
