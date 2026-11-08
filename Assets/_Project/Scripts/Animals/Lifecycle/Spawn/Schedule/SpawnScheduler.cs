using System;
using UnityEngine;
using ZooWorld.Shared;

namespace ZooWorld.Animals
{
    internal sealed class SpawnScheduler : ISpawnScheduler
    {
        private readonly SpawnSettings _settings;
        private readonly AnimalSelector _animalSelector;
        private readonly SpawnPointSelector _spawnPointSelector;
        private readonly IAnimalSpawner _spawner;
        private readonly IAnimalPopulation _population;
        private readonly IRandom _random;

        private float _secondsLeft;

        public SpawnScheduler(
            SpawnSettings settings,
            AnimalSelector animalSelector,
            SpawnPointSelector spawnPointSelector,
            IAnimalSpawner spawner,
            IAnimalPopulation population,
            IRandom random)
        {
            _settings = settings;
            _animalSelector = animalSelector;
            _spawnPointSelector = spawnPointSelector;
            _spawner = spawner;
            _population = population;
            _random = random;
            _secondsLeft = GetNextInterval();
        }

        public void Tick(float deltaTime)
        {
            _secondsLeft -= deltaTime;
            if (_secondsLeft > 0f)
            {
                return;
            }

            _secondsLeft = GetNextInterval();
            if (_population.Count < _settings.MaxAnimals)
            {
                SpawnSafely();
            }
        }

        private float GetNextInterval() => _random.Range(_settings.MinInterval, _settings.MaxInterval);

        private void SpawnSafely()
        {
            try
            {
                _spawner.Spawn(_animalSelector.Select(), _spawnPointSelector.Select());
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
            }
        }
    }
}
