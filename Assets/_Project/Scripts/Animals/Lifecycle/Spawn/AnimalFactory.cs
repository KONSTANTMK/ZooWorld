using ZooWorld.Shared;

namespace ZooWorld.Animals
{
    internal sealed class AnimalFactory
    {
        private readonly ICollisionQueue _collisionQueue;
        private readonly IScreenBounds _bounds;
        private readonly IRandom _random;
        private readonly AnimalLabel _fedLabel;

        private int _nextId;

        public AnimalFactory(
            ICollisionQueue collisionQueue,
            IScreenBounds bounds,
            IRandom random,
            AnimalLabel fedLabel)
        {
            _collisionQueue = collisionQueue;
            _bounds = bounds;
            _random = random;
            _fedLabel = fedLabel;
        }

        public Animal CreateAnimal(AnimalDefinition definition, IAnimalBodyComponent body)
        {
            _nextId++;
            var mover = new AnimalMover(body, _bounds, _random);
            return new Animal(_nextId, definition.Role, mover, definition.Movement.Create());
        }

        public AnimalPresenter CreatePresenter(Animal animal, AnimalView view)
        {
            return new AnimalPresenter(animal, view, _collisionQueue, _fedLabel);
        }
    }
}
