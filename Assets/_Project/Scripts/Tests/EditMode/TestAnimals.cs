using ZooWorld.Animals;

namespace ZooWorld.Tests
{
    internal static class TestAnimals
    {
        private const float Speed = 1f;
        private const float TurnInterval = 10f;

        private static int _nextId;

        public static Animal Create(AnimalRole role) => Create(role, new FakeBody());

        public static Animal Create(AnimalRole role, IAnimalBodyComponent body)
        {
            _nextId++;
            var mover = new AnimalMover(body, new FakeBounds(), new FakeRandom());
            return new Animal(_nextId, role, mover, new LinearMovementStrategy(Speed, TurnInterval));
        }
    }
}
