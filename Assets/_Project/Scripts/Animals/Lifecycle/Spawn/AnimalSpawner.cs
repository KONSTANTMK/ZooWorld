using System.Numerics;

namespace ZooWorld.Animals
{
    internal sealed class AnimalSpawner : IAnimalSpawner
    {
        private readonly AnimalPool _pool;
        private readonly AnimalPopulation _population;
        private readonly AnimalFactory _factory;

        public AnimalSpawner(AnimalPool pool, AnimalPopulation population, AnimalFactory factory)
        {
            _pool = pool;
            _population = population;
            _factory = factory;
        }

        public void Spawn(AnimalDefinition definition, Vector2 position)
        {
            AnimalView view = _pool.Get(definition);
            AnimalPresenter presenter = null;
            try
            {
                view.Body.Place(position);
                Animal animal = _factory.CreateAnimal(definition, view.Body);
                presenter = _factory.CreatePresenter(animal, view);
                _population.Register(animal, view, presenter);
            }
            catch
            {
                presenter?.Dispose();
                _pool.Release(view);
                throw;
            }
        }
    }
}
