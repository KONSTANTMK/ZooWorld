using UnityEngine;
using Zenject;

namespace ZooWorld.Animals
{
    public sealed class AnimalsInstaller
        : Installer<SpawnTable, SpawnSettings, InteractionTable, Transform, AnimalLabel, AnimalsInstaller>
    {
        private readonly SpawnTable _spawnTable;
        private readonly SpawnSettings _spawnSettings;
        private readonly InteractionTable _interactionTable;
        private readonly Transform _animalsRoot;
        private readonly AnimalLabel _fedLabel;

        public AnimalsInstaller(
            SpawnTable spawnTable,
            SpawnSettings spawnSettings,
            InteractionTable interactionTable,
            Transform animalsRoot,
            AnimalLabel fedLabel)
        {
            _spawnTable = spawnTable;
            _spawnSettings = spawnSettings;
            _interactionTable = interactionTable;
            _animalsRoot = animalsRoot;
            _fedLabel = fedLabel;
        }

        public override void InstallBindings()
        {
            BindLifecycle();
            BindSpawn();
            BindInteractions();
        }

        private void BindLifecycle()
        {
            Container.Bind<AnimalPool>().AsSingle().WithArguments(_animalsRoot);
            Container.BindInterfacesAndSelfTo<AnimalPopulation>().AsSingle();
            Container.Bind<AnimalFactory>().AsSingle().WithArguments(_fedLabel);
            Container.BindInterfacesTo<AnimalSpawner>().AsSingle();
            Container.BindInterfacesTo<AnimalDespawner>().AsSingle();
        }

        private void BindSpawn()
        {
            Container.BindInstance(_spawnTable);
            Container.BindInstance(_spawnSettings);
            Container.Bind<AnimalSelector>().AsSingle();
            Container.Bind<SpawnPointSelector>().AsSingle();
            Container.BindInterfacesTo<SpawnScheduler>().AsSingle();
        }

        private void BindInteractions()
        {
            Container.BindInstance(_interactionTable.CreateSelector());
            Container.BindInterfacesTo<CollisionQueue>().AsSingle();
        }
    }
}
