using UnityEngine;
using Zenject;
using ZooWorld.Animals;

namespace ZooWorld.App
{
    internal sealed class GameController : IFixedTickable
    {
        private readonly ISpawnScheduler _spawnScheduler;
        private readonly ICollisionQueue _collisionQueue;
        private readonly IAnimalDespawner _animalDespawner;
        private readonly IAnimalPopulation _animalPopulation;

        public GameController(
            ISpawnScheduler spawnScheduler,
            ICollisionQueue collisionQueue,
            IAnimalDespawner animalDespawner,
            IAnimalPopulation animalPopulation)
        {
            _spawnScheduler = spawnScheduler;
            _collisionQueue = collisionQueue;
            _animalDespawner = animalDespawner;
            _animalPopulation = animalPopulation;
        }

        public void FixedTick()
        {
            float deltaTime = Time.fixedDeltaTime;
            _spawnScheduler.Tick(deltaTime);
            _collisionQueue.DequeueAll();
            _animalDespawner.Despawn();
            _animalPopulation.Tick(deltaTime);
        }
    }
}
